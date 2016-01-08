using System.Collections.Generic;
using System.Threading.Tasks;
using RiotGames.Payments.Api.PaymentMethodApi.Models;

namespace RiotGames.Payments.Api.PaymentMethodApi.Services
{
    public interface IPaymentMethodService
    {
        Task<PaymentMethod> AddPaymentMethodAsync(PaymentMethod paymentMethod);
        Task AssignPaymentMethodIdAsync(PaymentMethod paymentMethod);
        Task<IEnumerable<PaymentMethod>> GetPaymentMethodsAsync(bool includeInactive = false);
        Task<PaymentMethod> GetPaymentMethodAsync(int id);
        Task InactivatePaymentMethodAsync(int id);
    }
}
