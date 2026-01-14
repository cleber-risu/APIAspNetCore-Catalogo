
using Microsoft.AspNetCore.Identity;

namespace ApiCatalogo.Models.Auth;

public class ApplicationUser : IdentityUser
{
  public string? RefreshToken { get; set; }
  public DateTime RefreshTokenExpiryTime { get; set; }
}
