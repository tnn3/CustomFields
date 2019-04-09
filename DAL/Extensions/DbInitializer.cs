﻿using System.Linq;
using System.Threading.Tasks;
using CustomFields.Domain;
using Domain;
using CustomFields.Domain.Enums;
using Microsoft.AspNetCore.Identity;

namespace DAL.Extensions
{
    public class DbInitializer
    {
        public static void Seed(ApplicationDbContext context, RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager)
        {
            context.Database.EnsureCreated();

            if (!context.Users.Any())
            {
                SeedUsers(roleManager, userManager).Wait();
            }

            if (!context.CustomFields.Any())
            {
                context.CustomFields.Add(new CustomField2
                {
                    FieldType = FieldType.Text,
                    FieldName = new FieldName { FieldDefaultName = "Test Text Field" },
                    IsRequired = true,
                    MaxLength = 10,
                    Sort = 1,
                    Status = FieldStatus.Active
                });
                context.CustomFields.Add(new CustomField2
                {
                    FieldType = FieldType.Checkbox,
                    FieldName = new FieldName { FieldDefaultName = "Test Checkbox" },
                    Sort = 2,
                    Status = FieldStatus.Active
                });
                context.CustomFields.Add(new CustomField2
                {
                    FieldType = FieldType.Radio,
                    FieldName = new FieldName { FieldDefaultName = "Test Radio" },
                    PossibleValues = "Radio Value4, Radio Value5, Radio Value6",
                    Sort = 3,
                    Status = FieldStatus.Active
                });
                context.CustomFields.Add(new CustomField2
                {
                    FieldType = FieldType.Select,
                    FieldName = new FieldName { FieldDefaultName = "Test Select" },
                    PossibleValues = "Select Value7, Select Value8, Select Value9",
                    Sort = 4,
                    Status = FieldStatus.Active
                });
                context.CustomFields.Add(new CustomField2
                {
                    FieldType = FieldType.Text,
                    FieldName = new FieldName { FieldDefaultName = "Old removed field" },
                    Sort = 0,
                    Status = FieldStatus.Hidden
                });
                context.CustomFields.Add(new CustomField2
                {
                    FieldType = FieldType.Textarea,
                    FieldName = new FieldName { FieldDefaultName = "Test textarea" },
                    Sort = 5,
                    Status = FieldStatus.Active
                });
                context.SaveChanges();
            }

            if (!context.ProjectTasks.Any())
            {
                context.ProjectTasks.Add(new ProjectTask{Title = "Test Task Title"});
                context.SaveChanges();
            }

            if (!context.CustomFieldInTasks.Any())
            {
                context.CustomFieldInTasks.Add(new CustomFieldInTasks {
                    FieldValue = "Test text field value", CustomFieldId = 1, ProjectTaskId = 1
                });
                context.CustomFieldInTasks.Add(new CustomFieldInTasks
                {
                    FieldValue = "on", CustomFieldId = 2, ProjectTaskId = 1
                });
                context.CustomFieldInTasks.Add(new CustomFieldInTasks
                {
                    FieldValue = "Radio Value5", CustomFieldId = 3, ProjectTaskId = 1
                });
                context.CustomFieldInTasks.Add(new CustomFieldInTasks
                {
                    FieldValue = "Select Value9", CustomFieldId = 4, ProjectTaskId = 1
                });
                context.CustomFieldInTasks.Add(new CustomFieldInTasks
                {
                    FieldValue = "Textarea long text", CustomFieldId = 6, ProjectTaskId = 1
                });
                context.SaveChanges();
            }
        }

        private static async Task SeedUsers(RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager)
        {
            const string roleAdmin = "admin";
            const string roleUser = "user";
            const string password = "Testing1";

            await roleManager.CreateAsync(new IdentityRole(roleAdmin));
            var appUser = new IdentityUser
            {
                UserName = "Admin",
                Email = "admin@test.ee"
            };
            await userManager.CreateAsync(appUser, password);
            await userManager.AddToRoleAsync(appUser, roleAdmin);

            await roleManager.CreateAsync(new IdentityRole(roleUser));
            appUser = new IdentityUser
            {
                UserName = "User",
                Email = "user@test.ee"
            };
            await userManager.CreateAsync(appUser, password);
            await userManager.AddToRoleAsync(appUser, roleUser);
        }
    }
}
