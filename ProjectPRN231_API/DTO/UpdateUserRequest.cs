using System.ComponentModel.DataAnnotations;

namespace ProjectPRN231_API.DTO;

public class UpdateUserRequest
{
    [Required]
    public int AccountId { get; set; }

    [Required]
    public string? FirstName { get; set; }

    [Required]
    public string? LastName { get; set; }

    [Required]
    public string? Address { get; set; }

    [Required]
    [EmailAddress]
    [StringLength(200)]
    public string? Email { get; set; }

    [Required]
    [StringLength(200)]
    public string? Phone { get; set; }

    [Required]
    public bool Gender { get; set; }

    [Required]
    public DateTime BirthDate { get; set; }
}
