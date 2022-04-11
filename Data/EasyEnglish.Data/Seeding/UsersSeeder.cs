namespace EasyEnglish.Data.Seeding
{
    using System;
    using System.Threading.Tasks;

    using EasyEnglish.Data.Models;

    public class UsersSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            await dbContext.Users.AddAsync(new ApplicationUser
            {
                Name = "Full Name",
                AddressText = "Plovdiv, 1 Test Str.",
                BirthDate = DateTime.Now,
                Email = "testUser@easyengish.com",
            });

            await dbContext.SaveChangesAsync();
        }
    }
}
