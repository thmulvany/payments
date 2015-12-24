using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using Payments.Api.PaymentMethodApi.Exceptions;
using Payments.Api.PaymentMethodApi.Models;
using Payments.Api.PaymentMethodApi.Services;

namespace Payments.Api.PaymentMethodApi.Controllers
{
    [Route("payments/api/v1/[controller]")]
    public class PaymentMethodsController : Controller
    {
        [FromServices]
        public IPaymentMethodService PaymentMethodService { get; set; }

        [HttpGet]
        public async Task<IEnumerable<PaymentMethod>> GetActive()
        {
            return await PaymentMethodService.GetPaymentMethodsAsync();
        }

        [HttpGet("/all")]
        public async Task<IEnumerable<PaymentMethod>> GetAll()
        {
            return await PaymentMethodService.GetPaymentMethodsAsync(true);
        }

        [HttpGet("{id:int}", Name = "GetPaymentMethod")]
        public async Task<PaymentMethod> GetById(int id)
        {
            return await PaymentMethodService.GetPaymentMethodAsync(id);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] PaymentMethod paymentMethod)
        {
            if (paymentMethod == null)
            {
                return HttpBadRequest();
            }
            try
            {
                await PaymentMethodService.AddPaymentMethodAsync(paymentMethod);
                return CreatedAtRoute("GetPaymentMethod", new { controller = "PaymentMethods", id = paymentMethod.Id },
                    paymentMethod);
            }
            catch (PaymentMethodInvalidException)
            {
                return HttpBadRequest();
            }
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] PaymentMethod paymentMethod)
        {
            // ensure the API call is legit
            if (paymentMethod == null || paymentMethod.Id != id)
                return HttpBadRequest();

            try
            {
                await PaymentMethodService.AssignPaymentMethodIdAsync(paymentMethod);
                return new NoContentResult();
            }
            catch (PaymentMethodNotFoundException)
            {
                return HttpNotFound();
            }
            catch (PaymentMethodInvalidException)
            {
                return HttpBadRequest();
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await PaymentMethodService.InactivatePaymentMethodAsync(id);
                return new HttpOkResult();
            }
            catch (PaymentMethodNotFoundException)
            {
                return HttpNotFound();
            }
        }
    }
}
