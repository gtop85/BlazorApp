using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using System.Threading.Tasks;

namespace BlazorApp
{
    public static class AuthExtensions
    {

        public static void GetDefaultSettings(this AuthenticationOptions options)
        {
            options.DefaultScheme = "Cookies";
            options.DefaultChallengeScheme = "oidc";
        }

        public static void GetCookieSettings(this OpenIdConnectOptions options, AuthSettings authSettings)
        {
            options.Authority = authSettings.Authority;
            options.ClientId = authSettings.ClientId;
            options.ClientSecret = authSettings.ClientSecret;
            foreach (var scope in authSettings.Scopes)
            {
                options.Scope.Add(scope);
            }
            //options.UsePkce = true;
            options.SaveTokens = true;
            options.ResponseType = "code";
            options.GetClaimsFromUserInfoEndpoint = true;

            options.Events = new OpenIdConnectEvents
            {
                OnAccessDenied = context =>
                {
                    context.HandleResponse();
                    context.Response.Redirect("/");
                    return Task.CompletedTask;
                }
            };
        }

        public static void GetBearerSettings(this JwtBearerOptions options, AuthSettings authSettings)
        {
            options.Authority = authSettings.Authority;
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false
            };
        }

        public static void GetAuthorizationSettings(this AuthorizationOptions options)
        {
            options.DefaultPolicy = new AuthorizationPolicyBuilder()
                .RequireAuthenticatedUser()
                .AddAuthenticationSchemes("Cookies", "Bearer")
                .Build();
        }
    }
}
