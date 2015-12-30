using System;
using System.ComponentModel.DataAnnotations;

namespace RiotGames.Payments.Api.PaymentMethodApi.Models
{
    /// <summary>
    /// This is the data model class that represents a persisted payment method
    /// </summary>
    public class PaymentMethod
    {
        public int Id { get; set; }

        [Required]
        public string PaymentMethodName { get; set; }
        [Required]
        public string PspName { get; set; }
        [Required]
        public string PaymentInstrumentName { get; set; }

        public string PaymentMethodId { get; set; }
        public string GatewayName { get; set; }

        public DateTime CreatedOn { get; set; }
        public bool Active { get; set; }
        public DateTime? InactivatedOn { get; set; }
    }
}
