namespace EasyEnglish.Data.Seeding.CustomSeeders
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using EasyEnglish.Common;
    using EasyEnglish.Data.Models;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.DependencyInjection;

    public class AcountsSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var roleManager = serviceProvider.GetRequiredService<RoleManager<ApplicationRole>>();

            // Create Admin
            await CreateUser(
                userManager,
                roleManager,
                GlobalConstants.AccountsSeeding.AdminEmail,
                GlobalConstants.AccountsSeeding.AdminName,
                GlobalConstants.AccountsSeeding.AdminProfilPic,
                GlobalConstants.AccountsSeeding.AdminPhoneNumber,
                GlobalConstants.AdministratorRoleName);

            // Create Manager
            await CreateUser(
                userManager,
                roleManager,
                GlobalConstants.AccountsSeeding.ManagerEmail,
                GlobalConstants.AccountsSeeding.ManagerName,
                GlobalConstants.AccountsSeeding.ManagerProfilPic,
                GlobalConstants.AccountsSeeding.ManagerPhoneNumber,
                GlobalConstants.ManagerRoleName);

            // Create Teacher1
            await CreateUser(
                userManager,
                roleManager,
                GlobalConstants.AccountsSeeding.Teacher1Email,
                GlobalConstants.AccountsSeeding.Teacher1Name,
                GlobalConstants.AccountsSeeding.Teacher1ProfilPic,
                GlobalConstants.AccountsSeeding.Teacher1PhoneNumber,
                GlobalConstants.TeacherRoleName);

            // Create Teacher2
            await CreateUser(
                userManager,
                roleManager,
                GlobalConstants.AccountsSeeding.Teacher2Email,
                GlobalConstants.AccountsSeeding.Teacher2Name,
                GlobalConstants.AccountsSeeding.Teacher2ProfilPic,
                GlobalConstants.AccountsSeeding.Teacher2PhoneNumber,
                GlobalConstants.TeacherRoleName);

            // Create User1
            await CreateUser(
                userManager,
                roleManager,
                GlobalConstants.AccountsSeeding.Student1Email,
                GlobalConstants.AccountsSeeding.Student1Name,
                GlobalConstants.AccountsSeeding.Student1ProfilPic,
                GlobalConstants.AccountsSeeding.Student1PhoneNumber);

            // Create User2
            await CreateUser(
                userManager,
                roleManager,
                GlobalConstants.AccountsSeeding.Student2Email,
                GlobalConstants.AccountsSeeding.Student2Name,
                GlobalConstants.AccountsSeeding.Student2ProfilPic,
                GlobalConstants.AccountsSeeding.Student2PhoneNumber);

            // Create User3
            await CreateUser(
                userManager,
                roleManager,
                GlobalConstants.AccountsSeeding.Student3Email,
                GlobalConstants.AccountsSeeding.Student3Name,
                GlobalConstants.AccountsSeeding.Student3ProfilPic,
                GlobalConstants.AccountsSeeding.Student3PhoneNumber);

            // Create User4
            await CreateUser(
                userManager,
                roleManager,
                GlobalConstants.AccountsSeeding.Student4Email,
                GlobalConstants.AccountsSeeding.Student4Name,
                GlobalConstants.AccountsSeeding.Student4ProfilPic,
                GlobalConstants.AccountsSeeding.Student4PhoneNumber);

            // Create User5
            await CreateUser(
                userManager,
                roleManager,
                GlobalConstants.AccountsSeeding.Student5Email,
                GlobalConstants.AccountsSeeding.Student5Name,
                GlobalConstants.AccountsSeeding.Student5ProfilPic,
                GlobalConstants.AccountsSeeding.Student5PhoneNumber);

            // Create User6
            await CreateUser(
                userManager,
                roleManager,
                GlobalConstants.AccountsSeeding.Student6Email,
                GlobalConstants.AccountsSeeding.Student6Name,
                GlobalConstants.AccountsSeeding.Student6ProfilPic,
                GlobalConstants.AccountsSeeding.Student6PhoneNumber);
        }

        private static async Task CreateUser(
            UserManager<ApplicationUser> userManager,
            RoleManager<ApplicationRole> roleManager,
            string email,
            string name = null,
            string profilPicture = null,
            string phoneNumber = null,
            string roleName = null)
        {
            var user = new ApplicationUser
            {
                UserName = email,
                Email = email,
                Name = name,
                ProfilePicture = profilPicture,
                PhoneNumber = phoneNumber,
            };

            var password = GlobalConstants.AccountsSeeding.Password;

            if (roleName != null)
            {
                var role = await roleManager.FindByNameAsync(roleName);

                if (!userManager.Users.Any(x => x.UserName == user.UserName))
                {
                    var result = await userManager.CreateAsync(user, password);

                    if (result.Succeeded)
                    {
                        await userManager.AddToRoleAsync(user, roleName);
                    }
                }
            }
            else
            {
                if (!userManager.Users.Any(x => x.Roles.Count() == 0))
                {
                    var result = await userManager.CreateAsync(user, password);
                }
            }
        }
    }
}
