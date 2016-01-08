using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.Entity;
using RiotGames.Payments.Api.PaymentMethodApi.Models;

namespace RiotGames.Payments.Api.PaymentMethodApi.Repositories
{
    public class PaymentMethodRepo : IPaymentMethodRepo
    {
        private readonly PaymentMethodContext _context;

        public PaymentMethodRepo(PaymentMethodContext context)
        {
            _context = context;
        }

        public async Task<PaymentMethod> CreatePaymentMethodAsync(PaymentMethod paymentMethod)
        {
            _context.PaymentMethods.Add(paymentMethod);
            await _context.SaveChangesAsync();
            return paymentMethod;
        }

        public async Task<PaymentMethod> GetPaymentMethodAsync(int id)
        {
            return await _context.PaymentMethods.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<PaymentMethod> GetPaymentMethodAsync(string paymentMethodId)
        {
            return await _context.PaymentMethods.FirstOrDefaultAsync(p => p.PaymentMethodId == paymentMethodId);
        }

        public async Task<IEnumerable<PaymentMethod>> GetPaymentMethodsAsync(bool includeInactive = false)
        {
            if (includeInactive)
                return await _context.PaymentMethods.ToListAsync();

            return await _context.PaymentMethods.Where(pm => pm.Active).ToListAsync();
        }

        public async Task UpdatePaymentMethodIdAsync(int id, string paymentMethodId)
        {
            var pm = await _context.PaymentMethods.FirstOrDefaultAsync(p => p.Id == id);
            pm.PaymentMethodId = paymentMethodId;
            await _context.SaveChangesAsync();
        }

        public async Task InactivatePaymentMethodAsync(int id, bool activeState, DateTime activeStateChangedOn)
        {
            var pm = await _context.PaymentMethods.FirstOrDefaultAsync(p => p.Id == id);
            pm.Active = activeState;
            pm.InactivatedOn = activeStateChangedOn;
            await _context.SaveChangesAsync();
        }
    }
}
