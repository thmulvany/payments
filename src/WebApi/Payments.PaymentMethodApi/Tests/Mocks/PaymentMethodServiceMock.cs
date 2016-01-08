using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LightMock;
using RiotGames.Payments.Api.PaymentMethodApi.Models;
using RiotGames.Payments.Api.PaymentMethodApi.Services;

namespace RiotGames.Payments.Api.PaymentMethodApi.Tests.Mocks
{
    public class PaymentMethodServiceMock : IPaymentMethodService
    {
        private readonly IInvocationContext<IPaymentMethodService> _context;

        public PaymentMethodServiceMock(IInvocationContext<IPaymentMethodService> context)
        {
            this._context = context;
        }
        public Task<PaymentMethod> AddPaymentMethodAsync(PaymentMethod paymentMethod)
        {
            return _context.Invoke(m => m.AddPaymentMethodAsync(paymentMethod));
        }

        public Task AssignPaymentMethodIdAsync(PaymentMethod paymentMethod)
        {
            throw new NotImplementedException();
        }

        public Task<PaymentMethod> GetPaymentMethodAsync(int id)
        {
            return _context.Invoke(m => m.GetPaymentMethodAsync(id));
        }

        public Task<IEnumerable<PaymentMethod>> GetPaymentMethodsAsync(bool includeInactive = false)
        {
            throw new NotImplementedException();
        }

        public Task InactivatePaymentMethodAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
