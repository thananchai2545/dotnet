
using System.ComponentModel.DataAnnotations.Schema;

namespace dotnet.Models;

[Table("categories")]
public class Categories
{
    [Column("id")]
    public int Id { get; set; }
    [Column("name", TypeName = "varchar(255)")]
    public required string Name { get; set; }
    [Column("description", TypeName = "text")]
    public string? Description { get; set; }
    [Column("created_at", TypeName = "datetime")]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    [Column("updated_at", TypeName = "datetime")]
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}