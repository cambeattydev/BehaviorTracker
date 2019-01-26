using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using BehaviorTracker.Client.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
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

        public AccountController(IUrlHelperFactory urlHelperFactory,
            IActionContextAccessor actionContextAccessor)
        {
            //_signInManager = signInManager;
            _urlHelper = urlHelperFactory.GetUrlHelper(actionContextAccessor.ActionContext);
        }

        [HttpPost("[action]")]
        [AllowAnonymous]
        public IActionResult Login()
        {
            var redirectUrl = _urlHelper.Action("LoginCallback", "Account");
            var properties = new AuthenticationProperties
            {
RedirectUri = redirectUrl,
Items = { {"LoginProvider", GoogleDefaults.DisplayName}}
            };
                
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

            var authenticationResult = await HttpContext.AuthenticateAsync();
            
//            var info = await _signInManager.GetExternalLoginInfoAsync();
//            if (info == null)
//            {
//                return null;
//            }

//            var existingUser = await _signInManager.UserManager.Users.FirstOrDefaultAsync(user =>
//                user.Email.ToLower() ==
//                info.Principal.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Email).Value);

//            if (existingUser == null)
//            {
//                try
//                {
//                    var user = new IdentityUser
//                    {
//                        Email = info.Principal.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Email)?.Value,
//                        UserName =
//                            $"{info.Principal.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.GivenName)?.Value}.{info.Principal.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Surname)?.Value}",
//                        PasswordHash = "defaultPassword123!"
//                    };
//                    var savedUser = await _signInManager.UserManager.CreateAsync(user);
//
//                    if (!savedUser.Succeeded)
//                    {
//                        return Redirect("/login");
//                    }
//
//                    existingUser = user;
//                }
//                catch (Exception ex)
//                {
//                    var test = ex;
//                    throw;
//                }
//            }

//            var loginAdded = await _signInManager.UserManager.AddLoginAsync(existingUser, info);
//            if (loginAdded.Succeeded)
//            {
//                // Sign in the user with this external login provider if the user already has a login.
//                var signedIn =
//                    await _signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, true, true);
//                if (signedIn.Succeeded)
//                {
//                    return Redirect("/");
//                }
//            }
            try
            {

                var claimsIdentity = new ClaimsIdentity(
                    authenticationResult.Principal.Claims, CookieAuthenticationDefaults.AuthenticationScheme);

                var authProperties = new AuthenticationProperties
                {
                    AllowRefresh = true,
                    // Refreshing the authentication session should be allowed.

                    ExpiresUtc = DateTimeOffset.UtcNow.AddDays(1).AddMinutes(-30),
                    // The time at which the authentication ticket expires. A 
                    // value set here overrides the ExpireTimeSpan option of 
                    // CookieAuthenticationOptions set with AddCookie.

                    IsPersistent = true,
                    // Whether the authentication session is persisted across 
                    // multiple requests. Required when setting the 
                    // ExpireTimeSpan option of CookieAuthenticationOptions 
                    // set with AddCookie. Also required when setting 
                    // ExpiresUtc.

                    IssuedUtc = DateTimeOffset.UtcNow
                    // The time at which the authentication ticket was issued.

                    //RedirectUri = <string>
                    // The full path or absolute URI to be used as an http 
                    // redirect response value.
                };

                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity),
                    authProperties);

                return Redirect(returnUrl ?? "/");
            }
            catch (Exception ex)
            {
               return Redirect("/login");
            }

        }

        [HttpGet("[action]")]
        [Authorize]
        public async Task<IActionResult> AuthorizationModel()
        {
//            var user = await _signInManager.UserManager.GetUserAsync(HttpContext.User);
//            var roles = await _signInManager.UserManager.GetRolesAsync(user);
//            return Ok(new AuthorizationModel()
//            {
//                Email = user.Email,
//                Username = user.UserName,
//                Roles = roles
//            });
throw new NotImplementedException();
        }
    }
}