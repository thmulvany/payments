using System;
using Microsoft.AspNet.Builder;

namespace RiotGames.Payments.Api.ApiKeyAuthentication
{
    public static class BasicAuthenticationExtensions
    {
        public static void UseBasicAuthentication(this IApplicationBuilder builder)
        {
            builder.UseMiddleware<BasicAuthentication>();
        }
    }
}