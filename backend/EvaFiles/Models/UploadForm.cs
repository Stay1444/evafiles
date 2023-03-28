using System.ComponentModel.DataAnnotations;

namespace EvaFiles.Models;

public class UploadForm
{
    [Required]
    public string Name { get; set; }
    [Required]
    public string Description { get; set; }
    [Required]
    public long Duration { get; set; }
    [Required]
    public IFormFile File { get; set; }
    
}