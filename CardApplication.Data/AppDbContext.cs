using CardApplication.Model;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace CardApplication.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}

        public DbSet<Payment> Payments { get; set; }
        public DbSet<Transactions> Transactions { get; set; }

        public override int SaveChanges()
        {
            var entries = ChangeTracker
                .Entries()
                .Where(e => e.Entity is Transactions && (
                        e.State == EntityState.Added
                        || e.State == EntityState.Modified));

            foreach (var entityEntry in entries)
            {
                ((Transactions)entityEntry.Entity).Status = PaymentStatus.pending;

                if (entityEntry.State == EntityState.Added)
                {
                    ((Transactions)entityEntry.Entity).Status = PaymentStatus.processed;
                }
                else
                {
                    ((Transactions)entityEntry.Entity).Status = PaymentStatus.failed;
                }
            }

            return base.SaveChanges();
        }
    }
}
