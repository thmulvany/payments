using System.Collections.Generic;
using System.Threading.Tasks;
using Payments.Api.PaymentMethodApi.Models;

namespace Payments.Api.PaymentMethodApi.Services
{
    public interface IPaymentMethodService
    {
        Task AddPaymentMethodAsync(PaymentMethod paymentMethod);
        Task AssignPaymentMethodIdAsync(PaymentMethod paymentMethod);
        Task<IEnumerable<PaymentMethod>> GetPaymentMethodsAsync(bool includeInactive = false);
        Task<PaymentMethod> GetPaymentMethodAsync(int id);
        Task InactivatePaymentMethodAsync(int id);
    }
}
