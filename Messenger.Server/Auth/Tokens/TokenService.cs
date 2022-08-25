using Messenger.Domains.Dtos;
using Microsoft.IdentityModel.Tokens;
using NuGet.Common;
using System.Security.Principal;

public class TokenService : ITokenService
{
    public string BuildToken(string key, string issuer, User user)
    {
        var claims = new[]
        {
            new Claim(ClaimTypes.Name, user.UserName)
        };

        var auidience = string.Concat(issuer);

        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

        var tokenDescriptor = new JwtSecurityToken(issuer, auidience, claims,
            expires: DateTime.UtcNow.AddDays(30),
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
    }

    public bool VerifyToken(string key, string issuer, string token, string userName)
    {
        if (string.IsNullOrEmpty(userName))
            return false;

        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));

        var validationParameters = new TokenValidationParameters()
        {
            IssuerSigningKey = securityKey,
            ValidAudience = issuer,
            ValidIssuer = issuer,
            ValidateLifetime = true,
            ValidateAudience = true,
            ValidateIssuer = true,
            ValidateIssuerSigningKey = true
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        SecurityToken validatedToken = null;
        try
        {
            tokenHandler.ValidateToken(token, validationParameters, out validatedToken);
        }
        catch (SecurityTokenException)
        {
            return false;
        }
        catch (Exception e)
        {
            throw;
        }


        if (validatedToken != null)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, userName)
            };

            var securityToken = (validatedToken as JwtSecurityToken);

            if (securityToken == null) { return false; }

            var claim = securityToken.Claims.FirstOrDefault(c => c.Value == userName);

            if (claim != null) { return true; }

        }


        return false;
    }

}