using System;
using System.Threading.Tasks;

namespace RiotGames.Payments.Api.ApiKeyAuthentication
{
    public interface IAuthenticationService
    {
        Task AuthenticateAsync(string username);
    }
}