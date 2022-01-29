using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Holidays.Models;

namespace holiday.Data;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<ApplicationUser>()
            .Property(e => e.firstName)
            .HasMaxLength(250);

        modelBuilder.Entity<ApplicationUser>()
            .Property(e => e.lastName)
            .HasMaxLength(250);

        modelBuilder.Entity<ApplicationUser>()
            .Property(e => e.allocation)
            .HasMaxLength(4);

    }

    public DbSet<Holidays.Models.Holiday> Holiday { get; set; }
}
