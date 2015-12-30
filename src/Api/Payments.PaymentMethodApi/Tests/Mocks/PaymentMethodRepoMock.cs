using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LightMock;
using RiotGames.Payments.Api.PaymentMethodApi.Models;
using RiotGames.Payments.Api.PaymentMethodApi.Repositories;

namespace RiotGames.Payments.Api.PaymentMethodApi.Tests.Mocks
{
    public class PaymentMethodRepoMock : IPaymentMethodRepo
    {
        private readonly IInvocationContext<IPaymentMethodRepo> _context;

        public PaymentMethodRepoMock(IInvocationContext<IPaymentMethodRepo> context)
        {
            _context = context;
        }

        public Task<PaymentMethod> CreatePaymentMethodAsync(PaymentMethod paymentMethod)
        {
            return _context.Invoke(m => m.CreatePaymentMethodAsync(paymentMethod));
        }

        public Task UpdatePaymentMethodIdAsync(int id, string paymentMethodId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<PaymentMethod>> GetPaymentMethodsAsync(bool includeInactive = false)
        {
            throw new NotImplementedException();
        }

        public Task<PaymentMethod> GetPaymentMethodAsync(int id)
        {
            return _context.Invoke(m => m.GetPaymentMethodAsync(id));
        }

        public Task<PaymentMethod> GetPaymentMethodAsync(string paymentMethodId)
        {
            return _context.Invoke(m => m.GetPaymentMethodAsync(paymentMethodId));
        }

        public Task InactivatePaymentMethodAsync(int id, bool activeState, DateTime activeStateChangedOn)
        {
            throw new NotImplementedException();
        }
    }
}
