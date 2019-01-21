using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.EntityFrameworkCore;

namespace BehaviorTracker.Server.Controllers
{
    [Route("api/[controller]")]
    public class AccountController : Controller
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IUrlHelper _urlHelper;

        public AccountController(SignInManager<IdentityUser> signInManager, IUrlHelperFactory urlHelperFactory,
            IActionContextAccessor actionContextAccessor)
        {
            _signInManager = signInManager;
            _urlHelper = urlHelperFactory.GetUrlHelper(actionContextAccessor.ActionContext);
        }

        [HttpPost("[action]")]
        [AllowAnonymous]
        public IActionResult Login()
        {
            var redirectUrl = _urlHelper.Action("LoginCallback", "Account");
            var properties = _signInManager.ConfigureExternalAuthenticationProperties(GoogleDefaults.DisplayName, redirectUrl);
            return Challenge(properties, GoogleDefaults.AuthenticationScheme);
        }

        [HttpGet("[action]")]
        [AllowAnonymous]
        public async Task<IActionResult> LoginCallback(string returnUrl = null, string remoteError = null)
        {
            if (remoteError != null)
            {
                return Redirect("/login");
            }

            var info = await _signInManager.GetExternalLoginInfoAsync();
            if (info == null)
            {
                return null;
            }

            var existingUser = _signInManager.UserManager.Users.FirstOrDefaultAsync(user =>
                user.Email.ToLower() == info.Principal.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Email).Value);

            if (existingUser == null)
            {
                var user = new IdentityUser
                {
                    Email = info.Principal.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Email)?.Value,
                    UserName =
                        $"{info.Principal.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Name)?.Value}.{info.Principal.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Surname)?.Value}"
                };
                var savedUser = await _signInManager.UserManager.CreateAsync(user);
                if (!savedUser.Succeeded)
                {
                    return Redirect("/login");
                }
            }

            // Sign in the user with this external login provider if the user already has a login.
            var signedIn = await _signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, true, true);
            if (signedIn.Succeeded)
            {
                return RedirectToPage("/");
            }

            // If the user does not have an account, then ask the user to create an account.
            return Redirect("/account/externalLogin");
        }
    }
}