using BrewLab.Common.Configuration;
using BrewLab.Common.DTOs;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace BrewLab.Common.JWT;
public class Token
{
    private static readonly JWTConfig JWTConfigs = Configs.JWT;
    private static readonly SymmetricSecurityKey StandardSSK = new(Encoding.UTF8.GetBytes(JWTConfigs.SymmetricSecurityKey));

    public static string GenerateToken(IDictionary<string, object> claims)
    {
        ArgumentNullException.ThrowIfNull(claims);

        var key = new SigningCredentials(StandardSSK, SecurityAlgorithms.HmacSha256);

        var descriptor = new SecurityTokenDescriptor
        {
            Expires = DateTime.Now.AddMinutes(JWTConfigs.JWTTokenDuration),
            NotBefore = DateTime.Now,
            Claims = claims,
            SigningCredentials = key
        };

        var handler = new JsonWebTokenHandler
        {
            SetDefaultTimesOnTokenCreation = false,
        };

        return handler.CreateToken(descriptor);
    }

    public static async Task<ExperimenterDTO.NameAndId?> ReadToken(string token)
    {
        var handler = new JsonWebTokenHandler();

        var tokenValidationParams = new TokenValidationParameters()
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = StandardSSK,
            ValidateIssuer = true,
            ValidIssuer = JWTConfigs.JWTIssuer,
            ValidateAudience = true,
            ValidAudience = JWTConfigs.JWTAudience,
            ValidateLifetime = true,
        };

        var result = await handler.ValidateTokenAsync(token, tokenValidationParams);

        if (result.IsValid)
        {
            if (result.Claims.FirstOrDefault(c => c.Key == "Id").Value is not int id) return null;
            if (result.Claims.FirstOrDefault(c => c.Key == "Username").Value is not string username) return null;

            return new ExperimenterDTO.NameAndId { Id = id, UserName = username };
        }
        else
        {
            return null;
        }
    }

    public static string GenerateToken(ExperimenterDTO.NameAndId user)
    {
        if (user.UserName is null) throw new ArgumentNullException(nameof(user));

        var claims = new Dictionary<string, object>
            {
                { "Username", user.UserName },
                { "Id", user.Id },
                { "iss", JWTConfigs.JWTIssuer },
                { "aud", JWTConfigs.JWTAudience }
            };

        return GenerateToken(claims);
    }
}
