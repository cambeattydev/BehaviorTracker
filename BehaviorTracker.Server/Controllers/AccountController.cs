using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;

namespace BehaviorTracker.Server.Controllers
{
    [Route("api/[controller]")]
    public class AccountController : Controller
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IUrlHelper _urlHelper;

        public AccountController(SignInManager<IdentityUser> signInManager, IUrlHelperFactory urlHelperFactory, IActionContextAccessor actionContextAccessor)
        {
            _signInManager = signInManager;
            _urlHelper = urlHelperFactory.GetUrlHelper(actionContextAccessor.ActionContext);
        }
        [HttpPost("[action]")]
        public IActionResult Login()
        {
            var redirectUrl = _urlHelper.Action("LoginCallback", "Account");
            var properties = _signInManager.ConfigureExternalAuthenticationProperties(GoogleDefaults.DisplayName, redirectUrl);
           return Challenge(properties, GoogleDefaults.AuthenticationScheme);

        }

        [HttpGet("[action]")]
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

            // Sign in the user with this external login provider if the user already has a login.
            var result = await _signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, isPersistent: false, bypassTwoFactor: true);
            if (result == null || result.IsNotAllowed)
            {
                return Redirect("/login");
            }

            if (result.Succeeded)
            {
                return RedirectToPage(returnUrl);
            }
            // If the user does not have an account, then ask the user to create an account.
            return Redirect("/account/externalLogin");
        }
    }
}