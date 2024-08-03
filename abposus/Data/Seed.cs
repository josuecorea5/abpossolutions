using abposus.Models;
using Microsoft.AspNetCore.Identity;

namespace abposus.Data
{
    public class Seed
    {
        public static async Task SeedUsersAndRolesAsync(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                if(!await roleManager.RoleExistsAsync(Roles.ADMIN.ToString()))
                    await roleManager.CreateAsync(new IdentityRole(Roles.ADMIN.ToString()));

                if(!await roleManager.RoleExistsAsync(Roles.ACCOUNTANT.ToString()))
                    await roleManager.CreateAsync(new IdentityRole(Roles.ACCOUNTANT.ToString()));

                if(!await roleManager.RoleExistsAsync(Roles.SELLER.ToString()))
                    await roleManager.CreateAsync(new IdentityRole(Roles.SELLER.ToString()));

                var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();
                string adminMail = "admin@abposus.com";

                var existUserEmail = await userManager.FindByEmailAsync(adminMail);

                if (existUserEmail is null)
                {
                    var newAdminUser = new AppUser()
                    {
                        UserName = "admin",
                        Email = adminMail,
                        FirstName = "David",
                        LastName = "Coreas",
                        Type = Roles.ADMIN,
                        EmailConfirmed = true,
                    };

                    await userManager.CreateAsync(newAdminUser, "Admin@2024");
                    await userManager.AddToRoleAsync(newAdminUser, Roles.ADMIN.ToString());
                }

                string sellerEmail = "seller@abposus.com";
                var existSellerEmail = await userManager.FindByEmailAsync(sellerEmail);

                if (existSellerEmail is null)
                {
                    var newSellerUser = new AppUser()
                    {
                        UserName = "seller",
                        Email = sellerEmail,
                        FirstName = "Juan",
                        LastName = "Paiz",
                        Type = Roles.SELLER,
                        EmailConfirmed = true,
                    };

                    await userManager.CreateAsync(newSellerUser, "Seller@2024");
                    await userManager.AddToRoleAsync(newSellerUser, Roles.SELLER.ToString());
                }

                string accountantEmail = "accountant@abposus.com";
                var existAccountantEmail = await userManager.FindByEmailAsync(accountantEmail);

                if (existAccountantEmail is null)
                {
                    var newAccountantUser = new AppUser()
                    {
                        UserName = "accountant",
                        Email = accountantEmail,
                        FirstName = "Pedro",
                        LastName = "Diaz",
                        Type = Roles.ACCOUNTANT,
                        EmailConfirmed = true,
                    };

                    await userManager.CreateAsync(newAccountantUser, "Accountant@2024");
                    await userManager.AddToRoleAsync(newAccountantUser, Roles.SELLER.ToString());
                }
            }
        }
    }
}
