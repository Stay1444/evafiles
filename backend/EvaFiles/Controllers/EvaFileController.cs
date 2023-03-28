using EvaFiles.Models;
using EvaFiles.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.EntityFrameworkCore;

namespace EvaFiles.Controllers;

[ApiController]
[Route("file")]
public sealed class EvaFileController : Controller
{
    private ILogger<EvaFileController> _logger;
    private EvaFilesDbContext _dbContext;
    private SeaweedClient _seaweed;
    
    public EvaFileController(ILogger<EvaFileController> logger, EvaFilesDbContext dbContext, SeaweedClient seaweed)
    {
        _logger = logger;
        _dbContext = dbContext;
        _seaweed = seaweed;
    }

    [HttpPost("upload")]
    public async Task<IActionResult> UploadRequest([FromForm] UploadForm model)
    {
        if (string.IsNullOrWhiteSpace(model.Name))
        {
            return BadRequest("Fields cannot be null / blank.");
        }

        _logger.LogInformation("Beginning upload {model}.\nFile Size: {size}", model, model.File.Length);

        var stream = model.File.OpenReadStream();
        var uploadResult = await _seaweed.UploadAsync(stream);
        await stream.DisposeAsync();

        var ef = new EvaFile
        {
            Id = Guid.NewGuid().ToString("N"),
            Name = model.Name,
            OriginalName = model.File.FileName,
            SeaHandle = uploadResult.Id,
            SeaVolume = uploadResult.VolumeUrl,
            ExpireDate = DateTimeOffset.UtcNow.AddSeconds(model.Duration).ToUnixTimeMilliseconds(),
            Duration = model.Duration,
            Size = model.File.Length
        };

        await _dbContext.Files.AddAsync(ef);
        await _dbContext.SaveChangesAsync();
        
        return Ok(new
        {
            Id = ef.Id,
            Name = ef.Name,
            OriginalName = ef.OriginalName,
            Size = ef.Size
        });
    }

    [HttpGet("download/{id}")]
    public async Task<IActionResult> DownloadRequest(string id)
    {
        var file = await _dbContext.Files.FirstOrDefaultAsync(x => x.Id == id);
        
        if (file is null) return NotFound("File not found");

        file.DownloadCount++;

        if (file.ExpireDate < DateTimeOffset.UtcNow.ToUnixTimeMilliseconds())
        {
            await _seaweed.DeleteAsync("http://" + file.SeaVolume + "/" + file.SeaHandle);
            _dbContext.Files.Remove(file);
            await _dbContext.SaveChangesAsync();
            
            return BadRequest("File was deleted.");
        }
        
        file.ExpireDate = DateTimeOffset.UtcNow.AddSeconds(file.Duration).ToUnixTimeMilliseconds();
        await _dbContext.SaveChangesAsync();
        
        var downloadClient = new HttpClient();

        var stream = await downloadClient.GetStreamAsync("http://" + file.SeaVolume + "/" + file.SeaHandle);
        new FileExtensionContentTypeProvider().TryGetContentType(file.OriginalName, out var contentType);
        return File(stream, contentType ?? "application/octet-stream");
    }

    [HttpPost("search")]
    public async Task<IActionResult> SearchRequest([FromForm] QueryModel query)
    {
        var files = await _dbContext.Files.Where(x => x.Name.ToLower().Contains(query.Query.ToLower())).OrderByDescending(x => x.DownloadCount).ToListAsync();

        return Json(files);
    }
}