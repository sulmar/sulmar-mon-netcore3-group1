using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Sulmar.Shopping.Domain;
using Sulmar.Shopping.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace Sulmar.Shopping.API.Handlers
{
    public class BasicAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        private readonly ICustomerRepositoryAsync customerRepository;

        public BasicAuthenticationHandler(
            IOptionsMonitor<AuthenticationSchemeOptions> options, 
            ILoggerFactory logger, 
            UrlEncoder encoder, 
            ISystemClock clock,
            ICustomerRepositoryAsync customerRepository
            ) : base(options, logger, encoder, clock)
        {
            this.customerRepository = customerRepository;
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if (!Request.Headers.ContainsKey("Authorization"))
            {
                return AuthenticateResult.Fail("Missing authorization header");
            }

            var authHeader = AuthenticationHeaderValue.Parse(Request.Headers["Authorization"]);

            if (authHeader.Scheme!="Basic")
            {
                return AuthenticateResult.Fail("Invalid Scheme");
            }

            var credentialBytes = Convert.FromBase64String(authHeader.Parameter);
            var credentials = Encoding.UTF8.GetString(credentialBytes).Split(':', 2);

            var username = credentials[0];
            var password = credentials[1];

            if (!customerRepository.TryAuthenticate(username, password, out Customer customer))
            {
                return AuthenticateResult.Fail("Invalid username or password");
            }

            ClaimsIdentity identity = new ClaimsIdentity(Scheme.Name);
            identity.AddClaim(new Claim(ClaimTypes.Name, username));
            identity.AddClaim(new Claim(ClaimTypes.Email, customer.Email));
            identity.AddClaim(new Claim("Pesel", customer.Pesel));

            identity.AddClaim(new Claim(ClaimTypes.Role, "developer"));
            identity.AddClaim(new Claim(ClaimTypes.Role, "trainer"));

            ClaimsPrincipal principal = new ClaimsPrincipal(identity);

            var ticket = new AuthenticationTicket(principal, Scheme.Name);

            return AuthenticateResult.Success(ticket);
        }
    }
}
