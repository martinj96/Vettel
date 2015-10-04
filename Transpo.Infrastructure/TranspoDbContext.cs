using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transpo.Core.Entities;

namespace Transpo.Infrastructure.Data
{
    public class TranspoDbContext : DbContext
    {
        public TranspoDbContext()
            : base("DefaultConnection")
        {

        }
        public TranspoDbContext(string cnnString)
            :base(cnnString)
        {

        }
        public DbSet<User> Users { get; set; }
        public DbSet<Car> Cars { get; set; }
        public DbSet<Characteristic> Characteristics { get; set; }
        public DbSet<CriticalPoint> CriticalPoints { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Ride> Rides { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            
            modelBuilder.Entity<Review>()
                .HasRequired<User>(r => r.Reviewer)
                .WithRequiredPrincipal();
            modelBuilder.Entity<Review>()
                .HasOptional<User>(r => r.Reviewer)
                .WithOptionalDependent();
            modelBuilder.Entity<User>()
                .HasMany<Characteristic>(u => u.Characteristics)
                .WithOptional();
            modelBuilder.Entity<User>()
                .HasMany<Ride>(u => u.Rides)
                .WithMany(r => r.Riders);
            modelBuilder.Entity<Ride>()
                .HasMany<CriticalPoint>(r => r.CriticalPoints)
                .WithMany(u => u.Rides);
            modelBuilder.Entity<User>()
                .HasOptional<Car>(u => u.Car)
                .WithOptionalPrincipal();
        } 
    }
}
