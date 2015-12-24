using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Payments.Api.PaymentMethodApi.Exceptions;
using Payments.Api.PaymentMethodApi.Models;
using Payments.Api.PaymentMethodApi.Repositories;

namespace Payments.Api.PaymentMethodApi.Services
{
    public class PaymentMethodService : IPaymentMethodService
    {
        private readonly IPaymentMethodRepo _repo;

        public PaymentMethodService(IPaymentMethodRepo repo)
        {
            _repo = repo;
        }

        public async Task AddPaymentMethodAsync(PaymentMethod paymentMethod)
        {
            // payment method name, psp name and payment instrument name are all required, everything else is optional
            if (string.IsNullOrEmpty(paymentMethod.PaymentMethodName) ||
                string.IsNullOrEmpty(paymentMethod.PspName) ||
                string.IsNullOrEmpty(paymentMethod.PaymentInstrumentName))
            {
                throw new PaymentMethodInvalidException(
                    "To create a new Payment Method the Payment Method Name, PSP Name and Payment Instrument Name are required.");
            }
            await _repo.CreatePaymentMethodAsync(paymentMethod);
        }

        public async Task<PaymentMethod> GetPaymentMethodAsync(int id)
        {
            return await _repo.GetPaymentMethodAsync(id);
        }

        public async Task<IEnumerable<PaymentMethod>> GetPaymentMethodsAsync(bool includeInactive = false)
        {
            return await _repo.GetPaymentMethodsAsync(includeInactive);
        }

        public async Task AssignPaymentMethodIdAsync(PaymentMethod paymentMethod)
        {
            // once a payment method has been created and updated once with a payment method ID, it becomes immutable

            var pm = await _repo.GetPaymentMethodAsync(paymentMethod.Id);

            // ensure there is something to potentially update
            if (pm == null)
                throw new PaymentMethodNotFoundException("Payment Method not found");

            // if payment method id already assigned then do not allow
            if (!string.IsNullOrEmpty(pm.PaymentMethodId))
                throw new PaymentMethodInvalidException("Payment Method has been created and assigned a payment method ID and is now immutable.");

            // at this point the update is allowed but a payment method id must have been provided 
            if (string.IsNullOrEmpty(paymentMethod.PaymentMethodId))
                throw new PaymentMethodInvalidException("Cannot update payment method. Payment method ID is required.");

            await _repo.UpdatePaymentMethodAsync(paymentMethod);
        }
        
        public async Task InactivatePaymentMethodAsync(int id)
        {
            var pm = await _repo.GetPaymentMethodAsync(id);
            
            // ensure there is something to potentially inactivate (delete)
            if (pm == null)
                throw new PaymentMethodNotFoundException("Payment Method not found");

            await _repo.DeletePaymentMethodAsync(id);
        }
    }
}
