using EmployeeManagementSystem.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagementSystem.Data
{
    // ApplicationDbContext ärver från IdentityDbContext för att inkludera 
    // användarhantering och autentisering
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // DbSet representerar en tabell i databasen
        public DbSet<Employee> Employees { get; set; }

        // OnModelCreating kan användas för att konfigurera modellen ytterligare
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Konfigurera EmployeeId för att INTE vara en identity column
            modelBuilder.Entity<Employee>()
                .Property(e => e.EmployeeId)
                .ValueGeneratedNever();  // Detta talar om för EF att inte generera värden för denna kolumn

            // Gör Notes-fältet nullable/optional
            modelBuilder.Entity<Employee>()
                .Property(e => e.Notes)
                .IsRequired(false);  // Detta gör fältet valfritt

            // Gör WorkingHours-fältet nullable/optional
            modelBuilder.Entity<Employee>()
                .Property(e => e.WorkingHours)
                .IsRequired(false);  // Detta gör fältet valfritt

            // Gör WorkPhone-fältet nullable/optional
            modelBuilder.Entity<Employee>()
                .Property(e => e.WorkPhone)
                .IsRequired(false);  // Detta gör fältet valfritt
        }
    }
}