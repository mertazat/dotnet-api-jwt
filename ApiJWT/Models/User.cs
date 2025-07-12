using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ApiJWT.Models;

[Table("users")]
[Index(nameof(Email), IsUnique = true)]
public record User
{
    [Key]
    public int Id
    {
        get; init;
    }
    [MaxLength(200)]
    [Required]
    public required string Email
    {
        get; init;
    }
    [Unicode(false)]
    [MaxLength(15)]
    [Required]
    public required string Phone
    {
        get; init;
    }
    [MaxLength(200)]
    [Required]
    public required string Name
    {
        get; init;
    }
    [MaxLength(200)]
    [Required]
    public required string Surname
    {
        get; init;
    }
    [MaxLength(500)]
    [Required]
    public required string Password
    {
        get; init;
    }
}