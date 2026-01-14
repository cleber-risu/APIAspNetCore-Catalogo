
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace ApiCatalogo.Services;

public interface ITokenService
{
  // retorna nosso token JWT
  JwtSecurityToken GenerateAccessToken(IEnumerable<Claim> claims, IConfiguration _config);
  // gera o RefreshToken
  string GenerateRefreshToken();
  // extrai as clains do token expirado para gerar o novo token usando o RefreshToken
  ClaimsPrincipal GetPrincipalFromExpiresToken(string token, IConfiguration _config);
}
