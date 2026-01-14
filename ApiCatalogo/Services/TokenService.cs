
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace ApiCatalogo.Services;

public class TokenService : ITokenService
{
  // gerar um novo token
  public JwtSecurityToken GenerateAccessToken(IEnumerable<Claim> claims, IConfiguration _config)
  {
    // pegamos as informações de validação do token
    var key = _config.GetSection("JWT").GetValue<string>("SecretKey") ?? throw new InvalidOperationException("Invalid secret key");
    // passamos as informações do formato texto para um array de bytes
    var privateKey = Encoding.UTF8.GetBytes(key);
    // criamos as credencias de assinatura, usando a partir da privateKey, e usando um algoritmo de encriptação
    var signingCredentials = new SigningCredentials(new SymmetricSecurityKey(privateKey), SecurityAlgorithms.HmacSha256Signature);

    // vamos vazer a construção do descritor do token
    var tokenDescriptor = new SecurityTokenDescriptor
    {
      // definimos os valores do token

      // claims relacionada com o usuario
      Subject = new ClaimsIdentity(claims),
      // data de expiração do token
      Expires = DateTime.UtcNow.AddMinutes(_config.GetSection("JWT").GetValue<double>("TokenValidityInMinutes")),
      // obter o valor da audiencia
      Audience = _config.GetSection("JWT").GetValue<string>("ValidAudience"),
      // obter o valor do emissor
      Issuer = _config.GetSection("JWT").GetValue<string>("ValidIssuer"),
      // atribuir as credenciais geradas usando a chave secreta usada para assinar o token
      SigningCredentials = signingCredentials
    };

    // criar uma instancia
    var tokenHandler = new JwtSecurityTokenHandler();
    // gerar o token
    var token = tokenHandler.CreateJwtSecurityToken(tokenDescriptor);

    return token;
  }

  public string GenerateRefreshToken()
  {
    // criamos um array de bytes, bytes aleatorios de forma segura
    var secureRandomBytes = new byte[128];
    // criamos um gerador de numeros aleatorios
    using var randomNumberGenerator = RandomNumberGenerator.Create();
    // preenchemos a variavel com bytes aleatorios
    randomNumberGenerator.GetBytes(secureRandomBytes);
    // convertemos os bytes aleatorios para um representação de string no formato Base64
    // para que token de atualização seja legivel, facil de armazenar ou transmitir
    var refreshToken = Convert.ToBase64String(secureRandomBytes);

    return refreshToken;
  }

  // usado para validar o token de acesso que foi expirado, o obter a claims principais dele
  public ClaimsPrincipal GetPrincipalFromExpiresToken(string token, IConfiguration _config)
  {
    var secretKey = _config["JWT:SecretKey"] ?? throw new InvalidOperationException("Invalid key");

    var tokenValidationParameters = new TokenValidationParameters
    {
      ValidateAudience = false,
      ValidateIssuer = false,
      ValidateIssuerSigningKey = true,
      // converte para um array de bytes e gera a chave de assinatura
      IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey)),
      ValidateLifetime = false
    };

    // criamos a instancia de JwtSecurityTokenHandler p manipular o token
    var tokenHandler = new JwtSecurityTokenHandler();
    // validamos o token com base nos parametros de validação
    // em out retorna o securityToken
    var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);
    // verificamos se o securityToken obtido pelo out não é uma instancia de JwtSecurityToken
    // e se o algoritmo de segurança não é o HmacSha256, assim lançando uma exceção
    if (securityToken is not JwtSecurityToken jwtSecurityToken ||
          !jwtSecurityToken.Header.Alg.Equals(
            SecurityAlgorithms.HmacSha256,
            StringComparison.InvariantCultureIgnoreCase
          ))
    {
      throw new SecurityTokenException("Invalid token");
    }

    return principal;
  }
}
