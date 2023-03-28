using EvaFiles.Models;
using Microsoft.EntityFrameworkCore;

namespace EvaFiles.Services;

public class EvaFilesDbContext : DbContext
{
    public EvaFilesDbContext(DbContextOptions<EvaFilesDbContext> options) : base(options)
    {
        
    }

    public DbSet<EvaFile> Files { get; set; } = null!; // null! to disable non initialized warning. EFCore will initialize it.

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<EvaFile>(x =>
        {
            x.HasKey(p => p.Id);
            x.ToTable("files");
        });
    }
}