using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RiotGames.Payments.Api.PaymentMethodApi.Models;

namespace RiotGames.Payments.Api.PaymentMethodApi.Repositories
{
    public interface IPaymentMethodRepo
    {
        Task<PaymentMethod> CreatePaymentMethodAsync(PaymentMethod paymentMethod);
        Task UpdatePaymentMethodIdAsync(int id, string paymentMethodId);
        Task<IEnumerable<PaymentMethod>> GetPaymentMethodsAsync(bool includeInactive = false);
        Task<PaymentMethod> GetPaymentMethodAsync(int id);
        Task<PaymentMethod> GetPaymentMethodAsync(string paymentMethodId);
        Task InactivatePaymentMethodAsync(int id, bool activeState, DateTime activeStateChangedOn);
    }
}
