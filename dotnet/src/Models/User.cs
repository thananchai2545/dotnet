using System.ComponentModel.DataAnnotations.Schema;

namespace dotnet.Models;

[Table("users")]
public class User
{
    [Column("id")]
    public int Id { get; set; }
    [Column("username" , TypeName = "varchar(255)")]
    public required string Username { get; set; }
    [Column("password" , TypeName = "varchar(255)")]
    public required string Password { get; set; }
    [Column("refresh_token" , TypeName ="text")]
    public string? RefreshToken { get; set; }
    [Column("refresh_token_expirytime", TypeName = "datetime")]
    public DateTime? RefreshTokenExpiryTime { get; set; }
    [Column("created_at")]    
    public DateTime CreatedAt { get; set; }
    [Column("updated_at")]
    public DateTime UpdatedAt { get; set; }

    // Additional properties can be added as needed 
}