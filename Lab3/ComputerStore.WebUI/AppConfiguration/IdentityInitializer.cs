using System.Threading.Tasks;
using ComputerStore.DataAccessLayer.Models.Identity;
using Microsoft.AspNetCore.Identity;

namespace ComputerStore.WebUI.AppConfiguration
{
    public class IdentityInitializer
    {
        private const string AdminEmail = "admin@gmail.com";
        private const string Password = "SACHEK1denis";

        public static async Task InitializeAsync(UserManager<IdentityBuyer> userManager, RoleManager<IdentityRole> roleManager)
        {
            if (await roleManager.FindByNameAsync(RolesNames.Admin) == null)
            {
                await roleManager.CreateAsync(new IdentityRole(RolesNames.Admin));
            }

            if (await roleManager.FindByNameAsync(RolesNames.User) == null)
            {
                await roleManager.CreateAsync(new IdentityRole(RolesNames.User));
            }

            if (await userManager.FindByNameAsync(AdminEmail) == null)
            {
                IdentityBuyer admin = new IdentityBuyer { Email = AdminEmail, UserName = AdminEmail, BuyerId = 1};
                IdentityResult result = await userManager.CreateAsync(admin, Password);

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(admin, RolesNames.Admin);
                }
            }
        }
    }
}