using CardApplication.Model;
using Microsoft.EntityFrameworkCore;

namespace CardApplication.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}

        public DbSet<Payment> Payments { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
    }
}
