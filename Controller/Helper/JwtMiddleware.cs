namespace Controller.Helper;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Service.Account;
using Service.Helpers;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

public class JwtMiddleware
{
    private readonly RequestDelegate _next;
    private readonly AppSetting _appSettings;

    public JwtMiddleware(RequestDelegate next, IOptions<AppSetting> appSettings)
    {
        _next = next;
        _appSettings = appSettings.Value;
    }

    public async Task Invoke(HttpContext context, IAccountService accountService)
    {
        var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

        if (token != null)
            attachUserToContext(context, accountService, token);

        await _next(context);
    }

    private void attachUserToContext(HttpContext context, IAccountService accountService, string token)
    {
        try
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false,
                // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
                ClockSkew = TimeSpan.Zero
            }, out SecurityToken validatedToken);

            var jwtToken = (JwtSecurityToken)validatedToken;
            var id = int.Parse(jwtToken.Claims.First(x => x.Type == "id").Value);

            // attach account to context on successful jwt validation
            context.Items["Account"] = accountService.GetById(id);
        }
        catch
        {
            // do nothing if jwt validation fails
            // user is not attached to context so request won't have access to secure routes
        }
    }
}

