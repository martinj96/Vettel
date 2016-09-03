using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transpo.Infrastructure.Data.Entities;
using Transpo.Infrastructure.Data.Identity;

namespace Transpo.Infrastructure.Data
{
    public class TranspoDbContext : IdentityDbContext<AppUser>, IDisposable
    {
        public TranspoDbContext()
            : base("DefaultConnection")
        {
        }
        public TranspoDbContext(string cnnString)
            :base(cnnString)
        {

        }
        public DbSet<User> UsersInfo { get; set; }
        public DbSet<Car> Cars { get; set; }
        //public DbSet<Characteristic> Characteristics { get; set; }
        public DbSet<CriticalPoint> CriticalPoints { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Ride> Rides { get; set; }
        public DbSet<OrderedCriticalPoint> OrderedCriticalPoints { get; set; }
        public DbSet<Message> Messages { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // User - Car
            modelBuilder.Entity<User>()
                .HasOptional<Car>(u => u.Car)
                .WithRequired(c => c.User)
                .WillCascadeOnDelete(false);

            // User - Review
            modelBuilder.Entity<User>()
                .HasMany<Review>(u => u.Reviews)
                .WithRequired(r => r.Reviewee)
                .HasForeignKey(s => s.RevieweeId)
                .WillCascadeOnDelete(false);

            // User - Ride
            modelBuilder.Entity<User>()
               .HasMany<Ride>(u => u.HasAccessToRides)
               .WithMany(r => r.UsersWithAccess)
               .Map(cs =>
               {
                   cs.MapLeftKey("UserId");
                   cs.MapRightKey("RideId");
                   cs.ToTable("UserWithAccessRide");
               });
            modelBuilder.Entity<User>()
                .HasMany<Ride>(u => u.MyRides)
                .WithRequired(r => r.Driver)
                .HasForeignKey(r => r.DriverId)
                .WillCascadeOnDelete(false); ;
            modelBuilder.Entity<User>()
               .HasMany<Ride>(u => u.Rides)
               .WithMany(r => r.Riders)
               .Map(cs =>
               {
                   cs.MapLeftKey("UserId");
                   cs.MapRightKey("RideId");
                   cs.ToTable("UserRide");
               });

            // Messages - Users
            modelBuilder.Entity<Message>()
                .HasRequired(x => x.Recipient)
                .WithMany(u => u.ReceivedMessages)
                .HasForeignKey(m => m.RecipientId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Message>()
                .HasRequired(x => x.Sender)
                .WithMany(u => u.SentMessages)
                .HasForeignKey(m => m.SenderId)
                .WillCascadeOnDelete(false);

            // Ride - OrderedCriticalPoint
            modelBuilder.Entity<Ride>()
                .HasMany<OrderedCriticalPoint>(r => r.OrderedCriticalPoints)
                .WithRequired(ocp => ocp.Ride)
                .HasForeignKey(r => r.RideId)
                .WillCascadeOnDelete(false);

            // CriticalPoint - OrderedCriticalPoint
            modelBuilder.Entity<CriticalPoint>()
                .HasMany<OrderedCriticalPoint>(cp => cp.OrderedCriticalPoints)
                .WithRequired(ocp => ocp.CriticalPoint)
                .HasForeignKey(ocp => ocp.CriticalPointId)
                .WillCascadeOnDelete(false);

            // Set Critical Points precision
            modelBuilder.Entity<CriticalPoint>().Property(p => p.Longitude).HasPrecision(10, 5);
            modelBuilder.Entity<CriticalPoint>().Property(p => p.Latitude).HasPrecision(10, 5);
        } 
    }
}
