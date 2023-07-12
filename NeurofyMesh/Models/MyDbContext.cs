using Microsoft.EntityFrameworkCore;

namespace NeurofyMesh.Models
{
    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
        {
        }

        public DbSet<Vendor> Vendors { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<VendorUser> VendorUsers { get; set; }
        public DbSet<Device> Devices { get; set; }
        public DbSet<Event> Events { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // use Fluent API configurations here
            modelBuilder.Entity<Vendor>(entity =>
            {
                entity.ToTable("Vendors").HasKey(e => e.VendorId);
                entity.Property(e => e.VendorId).ValueGeneratedOnAdd();

            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("Users").HasKey(e => e.UserId);
                entity.Property(e => e.UserId).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<VendorUser>(entity =>
            {
                entity.ToTable("VendorUsers").HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<Device>(entity =>
            {
                entity.ToTable("Devices").HasKey(e => e.DeviceId);
                entity.Property(e => e.DeviceId).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<Event>(entity =>
            {
                entity.ToTable("Events")
                .HasKey(e => e.EventId);

                entity.Property(e => e.EventId).ValueGeneratedOnAdd();

                entity.Property(e => e.EventType)
                .HasConversion
                (
                    a => a.ToString(),
                    a => (EventType)Enum.Parse(typeof(EventType), a)
                 );
            });
        }
    }
}
