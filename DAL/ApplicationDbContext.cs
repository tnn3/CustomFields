﻿using DAL.Extensions;
using Domain;
using Interfaces;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DAL
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Domain.ProjectTask> ProjectTasks { get; set; }
        public DbSet<Domain.CustomField2> CustomFields { get; set; }
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
