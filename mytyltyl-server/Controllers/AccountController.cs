using System.Security.Claims;
using Auth0.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Yuchiki.MytyltylApi;

namespace mytyltyl_server.Controllers
{
    [ApiController]
    [Route("/")]
    public class AccountController : Controller
    {
        [HttpGet("login")]
        public async Task Login(string returnUrl = "/")
        {
            AuthenticationProperties authenticationProperties = new LoginAuthenticationPropertiesBuilder()
                // Indicate here where Auth0 should redirect the user after a login.
                // Note that the resulting absolute Uri must be added to the
                // **Allowed Callback URLs** settings for the app.
                .WithRedirectUri(returnUrl)
                .Build();

            await HttpContext.ChallengeAsync(Auth0Constants.AuthenticationScheme, authenticationProperties);
        }


        [Authorize]
        [HttpGet("logout")]
        public async Task Logout()
        {
            AuthenticationProperties authenticationProperties = new LogoutAuthenticationPropertiesBuilder()
                .WithRedirectUri("/")
                .Build();

            await HttpContext.SignOutAsync(Auth0Constants.AuthenticationScheme, authenticationProperties);
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }

        [Authorize]
        [HttpGet("profile")]
        public ProfileResponse Profile()
        {
            return new ProfileResponse
            {
                Name = User.Identity?.Name,
                Email = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value,
                Icon = User.Claims.FirstOrDefault(c => c.Type == "picture")?.Value
            };
        }
    }



}
