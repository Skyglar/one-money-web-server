using System.ComponentModel.DataAnnotations;

namespace Identity.API.ViewModels.Account;

public sealed class LoginInputModel
{
    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; } = string.Empty;

    public bool RememberLogin { get; set; }
}
