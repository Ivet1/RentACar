using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RentACar.Models;

namespace RentACar.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Car> Cars { get; set; }
        public DbSet<RentalRequest> RentalRequests { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure decimal precision for PricePerDay property in Car entity
            modelBuilder.Entity<Car>()
                .Property(c => c.PricePerDay)
                .HasColumnType("decimal(18,2)");

            // Configure relationships
            modelBuilder.Entity<RentalRequest>()
                .HasOne(r => r.User) // A rental request belongs to one user
                .WithMany(u => u.RentalRequests) // A user can have many rental requests
                .HasForeignKey(r => r.UserId) // Foreign key in RentalRequest
                .OnDelete(DeleteBehavior.Cascade); // Optional: delete rental requests if user is deleted

            modelBuilder.Entity<RentalRequest>()
                .HasOne(r => r.Car) // A rental request belongs to one car
                .WithMany(c => c.RentalRequests) // A car can have many rental requests
                .HasForeignKey(r => r.CarId); // Foreign key in RentalRequest
        }
    }
}
