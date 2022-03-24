namespace EasyEnglish.Services.Data
{
    using System.Linq;

    using EasyEnglish.Data.Common.Repositories;
    using EasyEnglish.Data.Models;
    using EasyEnglish.Services.Data.Models;
    using EasyEnglish.Web.ViewModels.Home;

    public class GetCountsService : IGetCountsService
    {
        private readonly IDeletableEntityRepository<Course> coursesRepository;
        private readonly IDeletableEntityRepository<Student> studentsRepository;
        private readonly IDeletableEntityRepository<Teacher> teachersRepository;
        private readonly IDeletableEntityRepository<Language> languagesRepository;

        public GetCountsService(
            IDeletableEntityRepository<Course> coursesRepository,
            IDeletableEntityRepository<Student> studentsRepository,
            IDeletableEntityRepository<Teacher> teachersRepository,
            IDeletableEntityRepository<Language> languagesRepository)
        {
            this.coursesRepository = coursesRepository;
            this.studentsRepository = studentsRepository;
            this.teachersRepository = teachersRepository;
            this.languagesRepository = languagesRepository;
        }

        public CountsDto GetCounts()
        {
            var data = new CountsDto
            {
                CoursesCount = this.coursesRepository.AllAsNoTracking().Count(),
                StudentsCount = this.studentsRepository.AllAsNoTracking().Count(),
                TeachersCount = this.teachersRepository.AllAsNoTracking().Count(),
                LanguagesCount = this.languagesRepository.AllAsNoTracking().Count(),
            };

            return data;
        }
    }
}
