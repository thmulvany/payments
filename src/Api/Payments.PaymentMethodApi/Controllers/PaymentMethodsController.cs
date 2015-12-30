using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using Microsoft.Extensions.Logging;
using RiotGames.Payments.Api.PaymentMethodApi.Exceptions;
using RiotGames.Payments.Api.PaymentMethodApi.Models;
using RiotGames.Payments.Api.PaymentMethodApi.Services;

namespace RiotGames.Payments.Api.PaymentMethodApi.Controllers
{
    [Route("payments/api/v1/[controller]")]
    public class PaymentMethodsController : Controller
    {
        private readonly IPaymentMethodService _service;
        private readonly ILogger<PaymentMethodsController> _logger;

        public PaymentMethodsController(IPaymentMethodService service, ILogger<PaymentMethodsController> logger)
        {
            _logger = logger;
            _service = service;
        }

        [HttpGet]
        public async Task<IEnumerable<PaymentMethod>> GetActive()
        {
            return await _service.GetPaymentMethodsAsync();
        }

        [HttpGet("all")]
        public async Task<IEnumerable<PaymentMethod>> GetAll()
        {
            return await _service.GetPaymentMethodsAsync(true);
        }

        [HttpGet("{id:int}", Name = "GetPaymentMethod")]
        public async Task<PaymentMethod> GetById(int id)
        {
            _logger.LogInformation($"Received request for payment method with ID {id}");
            return await _service.GetPaymentMethodAsync(id);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] PaymentMethod paymentMethod)
        {
            if (!ModelState.IsValid)
            {
                return HttpBadRequest();
            }
            try
            {
                await _service.AddPaymentMethodAsync(paymentMethod);
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
                await _service.AssignPaymentMethodIdAsync(paymentMethod);
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
                await _service.InactivatePaymentMethodAsync(id);
                return new HttpOkResult();
            }
            catch (PaymentMethodNotFoundException)
            {
                return HttpNotFound();
            }
        }
    }
}
