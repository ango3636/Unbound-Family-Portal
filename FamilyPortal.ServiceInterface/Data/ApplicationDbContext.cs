using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using FamilyPortal.ServiceModel;

namespace FamilyPortal.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
    : IdentityDbContext<ApplicationUser>(options)
{
    public DbSet<Associate> Associate { get; set; }
    public DbSet<Child> Child { get; set; }
    public DbSet<Sponsorship> Sponsorship { get; set; }
    public DbSet<ELetter> ELetter { get; set; }
    public DbSet<DigitalChildLetter> DigitalChildLetter { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ELetter>()
            .Property(e => e.IsDraft)
            .HasDefaultValue(true);  // Default value is true for drafts

        base.OnModelCreating(modelBuilder);
    }
}