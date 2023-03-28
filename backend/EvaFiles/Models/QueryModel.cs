using System.ComponentModel.DataAnnotations;

namespace EvaFiles.Models;

public class QueryModel
{
    [Required]
    public string Query { get; set; }
}
