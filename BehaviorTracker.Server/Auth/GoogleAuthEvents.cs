using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OAuth;

namespace BehaviorTracker.Server.Auth
{
    internal class GoogleAuthEvents : OAuthEvents
    {
        private readonly string _domainName;

        public GoogleAuthEvents(string domainName)
        {
            this._domainName = domainName?.ToLower() ?? throw new ArgumentNullException(nameof(domainName));
        }

        public override Task RedirectToAuthorizationEndpoint(RedirectContext<OAuthOptions> context)
        {
            string GetRedirectUri()
            {
#if !DEBUG
                return $"{context.RedirectUri}&hd={_domainName}";
#endif
                return context.RedirectUri;
#endif
            }

            return base.RedirectToAuthorizationEndpoint(new RedirectContext<OAuthOptions>(
                context.HttpContext,
                context.Scheme,
                context.Options,
                context.Properties,
                GetRedirectUri()
            ));
        }

        public override Task TicketReceived(TicketReceivedContext context)
        {
            var emailClaim = context.Principal.Claims.FirstOrDefault(
                c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress");

            bool ValidateEmailWithDomain()
            {
#if !DEBUG
                !emailClaim.Value.ToLower().EndsWith(_domainName)
#endif
                return false;
#endif  
            }
            if (emailClaim == null || ValidateEmailWithDomain())
            {
                context.Response.StatusCode = 403; // or redirect somewhere
                context.HandleResponse();
            }

            return base.TicketReceived(context);
        }
    }
}