using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MuscleStore.Models;

namespace MuscleStore.Data
{
    public class MuscleStoreContext : DbContext
    {
        public MuscleStoreContext(DbContextOptions<MuscleStoreContext> options)
            : base(options)
        {
        }

        public DbSet<MuscleStore.Models.Brand> Brand { get; set; }

        public DbSet<MuscleStore.Models.Category> Category { get; set; }

        public DbSet<MuscleStore.Models.Product> Product { get; set; }

        public DbSet<MuscleStore.Models.Review> Review { get; set; }

        public DbSet<MuscleStore.Models.Account> Account { get; set; }

        public DbSet<MuscleStore.Models.Order> Order { get; set; }

        public DbSet<MuscleStore.Models.OrderDetail> OrderDetail { get; set; }

        public DbSet<MuscleStore.Models.ShoppingCart> ShoppingCart { get; set; }

        public DbSet<MuscleStore.Models.ShoppingCartItem> ShoppingCartItem { get; set; }

        public DbSet<MuscleStore.Models.Branch> Branch { get; set; }
    }
}
