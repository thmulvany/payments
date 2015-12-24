using System;
using Microsoft.AspNet.Builder;

namespace Payments.Api.ApiKeyAuthentication
{
    public static class BasicAuthenticationExtensions
    {
        public static void UseBasicAuthentication(this IApplicationBuilder builder)
        {
            builder.UseMiddleware<BasicAuthentication>();
        }
    }
}