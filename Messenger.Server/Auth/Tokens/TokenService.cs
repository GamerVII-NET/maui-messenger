using Microsoft.Net.Http.Headers;

public class TokenService : ITokenService
{
    public string BuildToken(string key, string issuer, User user)
    {
        var claims = new[]
        {
            new Claim(ClaimTypes.Name, user.UserName),
            new Claim(ClaimTypes.NameIdentifier, user.GlobalGuid.ToString())
        };

        var auidience = string.Concat(issuer);

        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

        var tokenDescriptor = new JwtSecurityToken(issuer, auidience, claims,
            expires: DateTime.UtcNow.AddDays(30),
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
    }

    public bool VerifyToken(WebApplicationBuilder builder, HttpContext context, User user)
    {
        string key = builder.Configuration["Jwt:Key"];
        string issuer = builder.Configuration["Jwt:Issuer"];
        string token = context.Request.Headers[HeaderNames.Authorization].ToString().Replace("Bearer ", "");

        if (string.IsNullOrEmpty(user.UserName) &&
            string.IsNullOrEmpty(user.GlobalGuid.ToString()))
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
/*            var claims = new[]
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.NameIdentifier, user.GlobalGuid.ToString())
            };*/

            var securityToken = (validatedToken as JwtSecurityToken);

            if (securityToken == null) { return false; }

            var claimUser = securityToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name);
            var claimGuid = securityToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);

            if (claimUser!.Value == user.UserName &&
                claimGuid!.Value == user.GlobalGuid.ToString())
            {
                return true;
            }

        }


        return false;
    }

}