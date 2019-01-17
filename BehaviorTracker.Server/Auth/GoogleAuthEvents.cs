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
            return base.RedirectToAuthorizationEndpoint(new RedirectContext<OAuthOptions>(
                context.HttpContext,
                context.Scheme,
                context.Options,
                context.Properties,
                $"{context.RedirectUri}&hd={_domainName}"));
        }

        public override Task TicketReceived(TicketReceivedContext context)
        {
            var emailClaim = context.Principal.Claims.FirstOrDefault(
                c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress");

            if (emailClaim == null || !emailClaim.Value.ToLower().EndsWith(_domainName))
            {
                context.Response.StatusCode = 403; // or redirect somewhere
                context.HandleResponse();
            }

            return base.TicketReceived(context);
        }
    }
}