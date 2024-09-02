using Microsoft.EntityFrameworkCore;
using RestaurantProject.Models;

namespace RestaurantProject.Data
{
    public class RestaurantContext : DbContext
    {
        public RestaurantContext(DbContextOptions<RestaurantContext> options) : base(options) { }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Table> Tables { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Dish> Dishes { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>()
                .HasIndex(c => c.PhoneNo)
                .IsUnique()
                .HasDatabaseName("Index_PhoneNo");


            modelBuilder.Entity<Dish>()
                .HasIndex(d => d.Name)
                .IsUnique()
                .HasDatabaseName("Index_Name");
        }
    }
}
