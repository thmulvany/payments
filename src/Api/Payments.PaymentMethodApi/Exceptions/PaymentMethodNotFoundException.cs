using System;
namespace RiotGames.Payments.Api.PaymentMethodApi.Exceptions
{
    public class PaymentMethodNotFoundException : Exception
    {
        public PaymentMethodNotFoundException() { }
        public PaymentMethodNotFoundException(string message) : base(message) { }
        public PaymentMethodNotFoundException(string message, System.Exception inner) : base(message, inner) { }

    }
}
