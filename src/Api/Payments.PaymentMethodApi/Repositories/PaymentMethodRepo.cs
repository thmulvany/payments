using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.Entity;
using Payments.Api.PaymentMethodApi.Models;

namespace Payments.Api.PaymentMethodApi.Repositories
{
    public class PaymentMethodRepo : IPaymentMethodRepo
    {
        private readonly PaymentMethodContext _context;

        public PaymentMethodRepo(PaymentMethodContext context)
        {
            _context = context;
        }

        public async Task<int> CreatePaymentMethodAsync(PaymentMethod paymentMethod)
        {
            _context.PaymentMethods.Add(paymentMethod);
            await _context.SaveChangesAsync();
            return paymentMethod.Id;
        }

        public async Task<PaymentMethod> GetPaymentMethodAsync(int id)
        {
            return await _context.PaymentMethods.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<PaymentMethod>> GetPaymentMethodsAsync(bool includeInactive = false)
        {
            if (includeInactive)
                return await _context.PaymentMethods.ToListAsync();

            return await _context.PaymentMethods.Where(pm => pm.Active).ToListAsync();
        }

        public async Task UpdatePaymentMethodAsync(PaymentMethod paymentMethod)
        {
            var pm = await _context.PaymentMethods.FirstOrDefaultAsync(p => p.Id == paymentMethod.Id);
            pm.PaymentMethodId = paymentMethod.PaymentMethodId;
            await _context.SaveChangesAsync();
        }

        public async Task DeletePaymentMethodAsync(int id)
        {
            var pm = await _context.PaymentMethods.FirstOrDefaultAsync(p => p.Id == id);
            
            // perform a soft delete
            pm.Active = false;
            pm.InactivatedOn = DateTime.UtcNow;

            await _context.SaveChangesAsync();
        }
    }
}
