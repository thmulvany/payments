using System;
using Microsoft.AspNet.Builder;

namespace RiotGames.Payments.Api.ApiKeyAuthentication
{
    public static class BasicAuthenticationExtensions
    {
        public static void UseApiKeyAuthentication(this IApplicationBuilder builder)
        {
            builder.UseMiddleware<BasicAuthentication>();
        }
    }
}