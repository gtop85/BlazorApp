using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace BlazorApp.Pages
{
    public class LoginModel : PageModel
    {
        public async Task OnGet(string redirectUri)
        {
            await HttpContext.ChallengeAsync("oidc", new
                AuthenticationProperties
            { RedirectUri = redirectUri ?? Url.Content("~/") });
        }
    }
}
