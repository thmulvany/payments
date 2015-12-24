using System;
using System.Threading.Tasks;

namespace Payments.Api.ApiKeyAuthentication
{
    public interface IAuthenticationService
    {
        Task AuthenticateAsync(string username);
    }
}