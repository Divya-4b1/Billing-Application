using BillingApp.Models;
using System.Collections.Generic;
using System.Reflection.Emit;
using Microsoft.EntityFrameworkCore;

namespace BillingApp.Contexts
{
    
    public class BillingContext : DbContext
    {
        public BillingContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<User> Users { get; set; }
        
        
    }
}


