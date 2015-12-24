using System;
using System.Threading.Tasks;
using Payments.Api.ApiKeyAuthentication;

namespace Payments.Api.ApiKeyAuthentication.Services
{
    public class ApiKeyAuthenticationService : IAuthenticationService
    {
        public Task AuthenticateAsync(string username)
        {
            if ("testuser".Equals(username, StringComparison.OrdinalIgnoreCase))
            {
                return Task.FromResult(0);
            }

            throw new InvalidCredentialsException();
        }
    }
}