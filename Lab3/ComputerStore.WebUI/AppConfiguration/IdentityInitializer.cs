using System.Threading.Tasks;
using ComputerStore.DataAccessLayer.Models.Identity;
using Microsoft.AspNetCore.Identity;

namespace ComputerStore.WebUI.AppConfiguration
{
    public class IdentityInitializer
    {
        private const string AdminEmail = "admin@gmail.com";
        private const string Password = "SACHEK1denis";
        private const string AdminRoleName = "admin";
        private const string UserRoleName = "user";

        public static async Task InitializeAsync(UserManager<IdentityBuyer> userManager, RoleManager<IdentityRole> roleManager)
        {
            if (await roleManager.FindByNameAsync(AdminRoleName) == null)
            {
                await roleManager.CreateAsync(new IdentityRole(AdminRoleName));
            }

            if (await roleManager.FindByNameAsync(UserRoleName) == null)
            {
                await roleManager.CreateAsync(new IdentityRole(UserRoleName));
            }

            if (await userManager.FindByNameAsync(AdminEmail) == null)
            {
                IdentityBuyer admin = new IdentityBuyer { Email = AdminEmail, UserName = AdminEmail, BuyerId = 1};
                IdentityResult result = await userManager.CreateAsync(admin, Password);

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(admin, AdminRoleName);
                }
            }
        }
    }
}