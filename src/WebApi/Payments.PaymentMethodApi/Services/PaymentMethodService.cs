using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using RiotGames.Payments.Api.PaymentMethodApi.Exceptions;
using RiotGames.Payments.Api.PaymentMethodApi.Models;
using RiotGames.Payments.Api.PaymentMethodApi.Repositories;

namespace RiotGames.Payments.Api.PaymentMethodApi.Services
{
    public class PaymentMethodService : IPaymentMethodService
    {
        private readonly IPaymentMethodRepo _repo;
        private readonly ILogger<PaymentMethodService> _logger;

        public PaymentMethodService(IPaymentMethodRepo repo, ILogger<PaymentMethodService> logger)
        {
            _repo = repo;
            _logger = logger;
        }

        public async Task<PaymentMethod> AddPaymentMethodAsync(PaymentMethod paymentMethod)
        {
            // payment method ID is a unique constraint (in addt to ID PK) so disallow duplicates
            var pm = await _repo.GetPaymentMethodAsync(paymentMethod.PaymentMethodId);
            if (pm != null)
                throw new PaymentMethodInvalidException("Payment Method already exists with this Payment Method ID. This is a unique identifier.");

            // activate and set created on
            paymentMethod.Active = true;
            paymentMethod.InactivatedOn = null;
            paymentMethod.CreatedOn = DateTime.UtcNow;

            await _repo.CreatePaymentMethodAsync(paymentMethod);

            return paymentMethod;
        }

        public async Task<PaymentMethod> GetPaymentMethodAsync(int id)
        {
            _logger.LogInformation($"Service processing get payment method for ID:{id}");
            PaymentMethod ret;
            try
            {
                ret = await _repo.GetPaymentMethodAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Cannot get payment method: error: {ex.Message}");
                throw;
            }
            return ret;
        }

        public async Task<IEnumerable<PaymentMethod>> GetPaymentMethodsAsync(bool includeInactive = false)
        {
            return await _repo.GetPaymentMethodsAsync(includeInactive);
        }

        public async Task AssignPaymentMethodIdAsync(PaymentMethod paymentMethod)
        {
            // at this point the update is allowed but a payment method id must have been provided 
            if (string.IsNullOrEmpty(paymentMethod.PaymentMethodId))
                throw new PaymentMethodInvalidException("Cannot update payment method. Payment method ID is required.");

            var pm = await _repo.GetPaymentMethodAsync(paymentMethod.Id);

            // ensure there is something to potentially update
            if (pm == null)
                throw new PaymentMethodNotFoundException("Payment Method not found");

            // once a payment method has been created and updated once with a payment method ID, it becomes immutable
            // if payment method id already assigned then do not allow (idempodent)
            if (!string.IsNullOrEmpty(pm.PaymentMethodId))
                throw new PaymentMethodInvalidException("Payment Method has been created and assigned a payment method ID and is now immutable.");

            // payment method ID is a unique constraint (in addt to ID PK) so disallow duplicates
            pm = await _repo.GetPaymentMethodAsync(paymentMethod.PaymentMethodId);
            if (pm != null)
                throw new PaymentMethodInvalidException("Payment Method already exists with this Payment Method ID. This is a unique identifier.");

            await _repo.UpdatePaymentMethodIdAsync(paymentMethod.Id, paymentMethod.PaymentMethodId);
        }
        
        public async Task InactivatePaymentMethodAsync(int id)
        {
            var pm = await _repo.GetPaymentMethodAsync(id);
            
            // ensure there is something to potentially inactivate (delete)
            if (pm == null)
                throw new PaymentMethodNotFoundException("Payment Method not found");
            
            // perform a soft delete
            await _repo.InactivatePaymentMethodAsync(id, false, DateTime.UtcNow);
        }
    }
}
