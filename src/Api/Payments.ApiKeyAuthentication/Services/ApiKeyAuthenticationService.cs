using System;
using System.Threading.Tasks;

namespace RiotGames.Payments.Api.ApiKeyAuthentication.Services
{
    public class ApiKeyAuthenticationService : IAuthenticationService
    {
        public Task AuthenticateAsync(string username)
        {
            // TODO: get api key from db
            if ("testuser".Equals(username, StringComparison.OrdinalIgnoreCase))
            {
                return Task.FromResult(0);
            }
            throw new InvalidCredentialsException();
        }
    }
}