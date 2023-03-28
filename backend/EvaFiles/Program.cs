using System.Net;
using EvaFiles.Services;
using EvaFiles.Utils.Configuration;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHttpClient(SeaweedClient.ClientName,
    c => c.BaseAddress = new Uri(Environment.GetEnvironmentVariable("SEAWEEDFS_URL") ?? builder.Configuration.GetString("SEAWEEDFS_URL", "http://127.0.0.1:9333")));


builder.Services.AddSingleton<SeaweedClient>();

builder.Services.AddNpgsql<EvaFilesDbContext>(builder.Configuration.GetPostgresConnectionString());

builder.WebHost.UseKestrel(x =>
{
    x.Listen(IPAddress.Any, 5000);
    x.Limits.MaxRequestBodySize = long.MaxValue;
});

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "cors",
        c =>
        {
            c.AllowAnyOrigin();
        });
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("cors");
app.MapControllers();

{
    using var scope = app.Services.CreateScope();
    var dbContext = scope.ServiceProvider.GetRequiredService<EvaFilesDbContext>();
    await dbContext.Database.MigrateAsync();
}

app.Run();