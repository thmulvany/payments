using System;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Infrastructure;
using RiotGames.Payments.Api.PaymentMethodApi.Repositories;

namespace RiotGames.Payments.Api.PaymentMethodApi.Migrations
{
    [DbContext(typeof(PaymentMethodContext))]
    partial class PaymentMethodContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0-rc1-16348");

            modelBuilder.Entity("RiotGames.Payments.Api.PaymentMethodApi.Models.PaymentMethod", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("Relational:ColumnName", "ID");

                    b.Property<bool>("Active");

                    b.Property<DateTime>("CreatedOn");

                    b.Property<string>("GatewayName");

                    b.Property<DateTime?>("InactivatedOn");

                    b.Property<string>("PaymentInstrumentName")
                        .IsRequired();

                    b.Property<string>("PaymentMethodId")
                        .HasAnnotation("Relational:ColumnName", "PaymentMethodID");

                    b.Property<string>("PaymentMethodName")
                        .IsRequired();

                    b.Property<string>("PspName")
                        .IsRequired();

                    b.HasKey("Id");
                });
        }
    }
}
