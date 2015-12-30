using System;
namespace RiotGames.Payments.Api.PaymentMethodApi.Exceptions
{
    public class PaymentMethodInvalidException : Exception
    {
        public PaymentMethodInvalidException() { }
        public PaymentMethodInvalidException(string message) : base(message) { }
        public PaymentMethodInvalidException(string message, System.Exception inner) : base(message, inner) { }

    }
}
