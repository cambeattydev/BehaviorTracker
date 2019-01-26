using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using BehaviorTracker.Client.Models;
using BehaviorTracker.Service.Interfaces;
using BehaviorTracker.Service.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.EntityFrameworkCore;
using AuthorizationModel = BehaviorTracker.Client.Models.AuthorizationModel;

namespace BehaviorTracker.Server.Controllers
{
    [Route("api/[controller]")]
    public class AccountController : Controller
    {
        private readonly IUrlHelper _urlHelper;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public AccountController(IUrlHelperFactory urlHelperFactory,
            IActionContextAccessor actionContextAccessor, IUserService userService, IMapper mapper)
        {
            _urlHelper = urlHelperFactory.GetUrlHelper(actionContextAccessor.ActionContext);
            _userService = userService;
            _mapper = mapper;
        }

        [HttpPost("[action]")]
        [AllowAnonymous]
        public IActionResult Login()
        {
            var redirectUrl = _urlHelper.Action("LoginCallback", "Account");
            var properties = new AuthenticationProperties
            {
                RedirectUri = redirectUrl,
                Items = {{"LoginProvider", GoogleDefaults.DisplayName}}
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

            if (!authenticationResult.Succeeded) return Redirect("/login");

            var roles = await _userService.LoginUserAsync(authenticationResult);
            
            try
            {
                var combinedClaims =
                    authenticationResult.Principal.Claims.Concat(roles.Select(role =>
                        new Claim(ClaimTypes.Role, role)));
                var claimsIdentity =
                    new ClaimsIdentity(combinedClaims, CookieAuthenticationDefaults.AuthenticationScheme);

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
            if (!HttpContext.User.Identity.IsAuthenticated) return Unauthorized();

            var authorizationModel = await _userService.GetUserRoles(HttpContext.User.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Email)
                ?.Value);

            if (authorizationModel == null)
            {
                return Unauthorized();
            }
            
            return Ok(_mapper.Map<AuthorizationModel>(authorizationModel));

        }
    }
}