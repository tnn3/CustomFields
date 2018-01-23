using System.Linq;
using System.Threading.Tasks;
using Domain;
using Microsoft.AspNetCore.Identity;

namespace DAL.Extensions
{
    public class DbInitializer
    {
        public static async Task Seed(ApplicationDbContext context, RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
        {
            context.Database.EnsureCreated();

            if (!context.Users.Any())
            {
                await SeedUsers(roleManager, userManager);
            }
        }

        private static async Task SeedUsers(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
        {
            const string roleAdmin = "admin";
            const string roleUser = "user";
            const string password = "Testing1";

            await roleManager.CreateAsync(new IdentityRole(roleAdmin));
            var appUser = new ApplicationUser
            {
                UserName = "Admin",
                Email = "admin@test.ee"
            };
            await userManager.CreateAsync(appUser, password);
            await userManager.AddToRoleAsync(appUser, roleAdmin);

            await roleManager.CreateAsync(new IdentityRole(roleUser));
            appUser = new ApplicationUser
            {
                UserName = "User",
                Email = "user@test.ee"
            };
            await userManager.CreateAsync(appUser, password);
            await userManager.AddToRoleAsync(appUser, roleUser);
        }
    }
}
