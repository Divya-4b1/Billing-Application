
using BillingApp.Models;
using Microsoft.EntityFrameworkCore;

namespace BillingApp.Contexts
{

    public class BillingContext : DbContext
    {
        public BillingContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Bill> Bills { get; set; }
        public DbSet<BillItems> BillItems { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Fluent API
            modelBuilder.Entity<Bill>(bill =>
            {
                bill.HasKey(ck => ck.billNumber);
            });
            modelBuilder.Entity<BillItems>(ci =>
            {
                ci.HasKey(cik => new { cik.BillNumber, cik.Product_Id });
            });
            modelBuilder.Entity<BillItems>()
                .HasOne<Product>(ci => ci.Product)
                .WithMany(p => p.BillItems);
        }
    }
}

    




