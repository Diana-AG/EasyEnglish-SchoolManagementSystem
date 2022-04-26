namespace EasyEnglish.Services.Data
{
    using EasyEnglish.Data.Common.Repositories;
    using EasyEnglish.Data.Models;
    using EasyEnglish.Services.Mapping;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class StudentsService : IStudentsService
    {
        private readonly IDeletableEntityRepository<ApplicationUser> usersRepository;

        public StudentsService(IDeletableEntityRepository<ApplicationUser> usersRepository)
        {
            this.usersRepository = usersRepository;
        }

        public async Task<IEnumerable<T>> GetAllActive<T>()
        {
            return await this.usersRepository.All()
                .Where(x => x.StudentCourses.Any(c => c.EndDate == null))
                .OrderBy(x => x.Name)
                .To<T>()
                .ToListAsync();
        }

        public async Task<IEnumerable<string>> GetAllEmailsAsync()
        {
            return await this.usersRepository.All()
                .Select(x => x.Email)
                .ToListAsync();
        }

        public async Task<IEnumerable<T>> GetAllInCourse<T>(int courseId)
        {
            return await this.usersRepository.All()
                .Where(x => x.StudentCourses.Any(sc => sc.Id == courseId))
                .OrderBy(x => x.Name)
                .To<T>()
                .ToListAsync();
        }
    }
}
