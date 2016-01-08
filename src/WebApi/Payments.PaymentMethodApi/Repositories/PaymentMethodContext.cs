using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Infrastructure;
using RiotGames.Payments.Api.PaymentMethodApi.Models;

namespace RiotGames.Payments.Api.PaymentMethodApi.Repositories
{
    public class PaymentMethodContext : DbContext
    {
        public PaymentMethodContext(DbContextOptions options) : base(options) { }
        public DbSet<PaymentMethod> PaymentMethods { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            new PaymentMethodMap(modelBuilder.Entity<PaymentMethod>());
        }
    }
}
