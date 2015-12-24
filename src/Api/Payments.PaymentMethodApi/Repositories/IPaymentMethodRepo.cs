using System.Collections.Generic;
using System.Threading.Tasks;
using Payments.Api.PaymentMethodApi.Models;

namespace Payments.Api.PaymentMethodApi.Repositories
{
    public interface IPaymentMethodRepo
    {
        Task<int> CreatePaymentMethodAsync(PaymentMethod paymentMethod);
        Task UpdatePaymentMethodAsync(PaymentMethod paymentMethod);
        Task<IEnumerable<PaymentMethod>> GetPaymentMethodsAsync(bool includeInactive = false);
        Task<PaymentMethod> GetPaymentMethodAsync(int id);
        Task DeletePaymentMethodAsync(int id);
    }
}
