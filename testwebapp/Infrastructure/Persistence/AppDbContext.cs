using Microsoft.EntityFrameworkCore;
using testwebapp.Domain.Entities;

namespace testwebapp.Infrastructure.Persistence;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Hotel> Hotels => Set<Hotel>();
    public DbSet<Room> Rooms => Set<Room>();
    public DbSet<RatePlan> RatePlans => Set<RatePlan>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Hotel>(e =>
        {
            e.HasKey(h => h.Id);
            e.Property(h => h.Name).HasMaxLength(200).IsRequired();
            e.Property(h => h.Description).HasMaxLength(1000);
        });

        modelBuilder.Entity<Room>(e =>
        {
            e.HasKey(r => r.Id);
            e.Property(r => r.Name).HasMaxLength(200).IsRequired();
            e.HasOne(r => r.Hotel)
             .WithMany(h => h.Rooms)
             .HasForeignKey(r => r.HotelId)
             .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<RatePlan>(e =>
        {
            e.HasKey(rp => rp.Id);
            e.Property(rp => rp.Name).HasMaxLength(200).IsRequired();
            e.Property(rp => rp.PricePerNight).HasColumnType("numeric(18,2)");
            e.Property(rp => rp.Currency).HasMaxLength(3).IsRequired();
            e.HasOne(rp => rp.Room)
             .WithMany(r => r.RatePlans)
             .HasForeignKey(rp => rp.RoomId)
             .OnDelete(DeleteBehavior.Cascade);
        });
    }
}
