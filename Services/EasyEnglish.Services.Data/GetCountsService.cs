namespace EasyEnglish.Services.Data
{
    using EasyEnglish.Common;
    using EasyEnglish.Data.Common.Repositories;
    using EasyEnglish.Data.Models;
    using EasyEnglish.Services.Data.Models;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using System.Linq;

    public class GetCountsService : IGetCountsService
    {
        private readonly IDeletableEntityRepository<Course> coursesRepository;
        private readonly IDeletableEntityRepository<ApplicationUser> usersRepository;
        private readonly IDeletableEntityRepository<Language> languagesRepository;
        private readonly IRepository<ApplicationRole> rolesRepo;

        public GetCountsService(
            IDeletableEntityRepository<Course> coursesRepository,
            IDeletableEntityRepository<ApplicationUser> usersRepository,
            IDeletableEntityRepository<Language> languagesRepository,
            IRepository<ApplicationRole> rolesRepo)
        {
            this.coursesRepository = coursesRepository;
            this.usersRepository = usersRepository;
            this.languagesRepository = languagesRepository;
            this.rolesRepo = rolesRepo;
        }

        public CountsDto GetCounts()
        {
            var teacherRoleId = this.rolesRepo.AllAsNoTracking()
                .Where(r => r.Name == GlobalConstants.TeacherRoleName)
                .Select(r => r.Id)
                .FirstOrDefault();

            var data = new CountsDto
            {
                CoursesCount = this.coursesRepository.AllAsNoTracking().Count(),
                TeachersCount = this.usersRepository.AllAsNoTracking().Count(x => x.Roles.Any(r => r.RoleId == teacherRoleId)),
                StudentsCount = this.usersRepository.AllAsNoTracking().Count(x => x.StudentCourses.Any()),
                LanguagesCount = this.languagesRepository.AllAsNoTracking().Count(),
            };

            return data;
        }
    }
}
