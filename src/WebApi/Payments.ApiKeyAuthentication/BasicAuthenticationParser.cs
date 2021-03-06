﻿using System;
using System.Linq;
using System.Text;
using Microsoft.AspNet.Http;
using Microsoft.Extensions.Primitives;

namespace RiotGames.Payments.Api.ApiKeyAuthentication
{
    internal class BasicAuthenticationParser
    {
        private readonly string credentials;

        public BasicAuthenticationParser(HttpContext context)
        {
            credentials = GetCredentials(context);
        }

        public string GetUsername()
        {
            return GetValue(credentials, 0);
        }

        private static string GetValue(string credentials, int index)
        {
            if (string.IsNullOrWhiteSpace(credentials))
                return null;

            var parts = credentials.Split(':');
            return parts[index];
        }

        private static string GetCredentials(HttpContext context)
        {
            try
            {
                StringValues authHeader;
                if (context.Request.Headers.TryGetValue("Authorization", out authHeader) &&
                    authHeader.Any() &&
                    authHeader[0].StartsWith("Token "))
                {
                    var value = Convert.FromBase64String(authHeader[0].Split(' ')[1]);
                    return Encoding.UTF8.GetString(value);
                }

                return null;
            }
            catch
            {
                return null;
            }
        }
    }
}