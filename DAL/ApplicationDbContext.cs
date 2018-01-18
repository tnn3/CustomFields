using DAL.Extensions;
using Domain;
using Interfaces;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DAL
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>, IDbContext
    {
        public DbSet<Domain.ProjectTask> ProjectTasks { get; set; }
        public DbSet<Domain.ApplicationUser> AppUsers { get; set; }
        public DbSet<Domain.CustomField> CustomFields { get; set; }
        public DbSet<Domain.CustomFieldInTasks> CustomFieldInTasks { get; set; }

        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.DisableCascadingDeletes();
        }
    }
}
