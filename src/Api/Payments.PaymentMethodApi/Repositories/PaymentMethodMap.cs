using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Metadata.Builders;
using Payments.Api.PaymentMethodApi.Models;

namespace Payments.Api.PaymentMethodApi.Repositories
{
    public class PaymentMethodMap
    {
        public PaymentMethodMap(EntityTypeBuilder<PaymentMethod> eb)
        {
            eb.Property(x => x.Id).ValueGeneratedOnAdd().HasColumnName("ID");
            eb.Property(x => x.PaymentMethodId).HasColumnName("PaymentMethodID");
        }
    }
}