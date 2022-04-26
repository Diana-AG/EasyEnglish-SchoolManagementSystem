namespace EasyEnglish.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using EasyEnglish.Data.Common.Repositories;
    using EasyEnglish.Data.Models;
    using EasyEnglish.Services.Mapping;
    using EasyEnglish.Web.ViewModels.Administration.Students;
    using Microsoft.EntityFrameworkCore;

    public class StudentsService : IStudentsService
    {
        private readonly string[] allowedExtensions = new[] { "jpg", "png" };

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

        public async Task<IEnumerable<T>> GetAllActive<T>(int page, int itemsPerPage = 8)
        {
            return await this.usersRepository.AllAsNoTracking()
               .Where(x => x.StudentCourses.Any(c => c.EndDate == null))
               .OrderBy(x => x.Name)
               .Skip((page - 1) * itemsPerPage).Take(itemsPerPage)
               .To<T>().ToListAsync();
        }

        public async Task<IEnumerable<T>> GetAllByTeacher<T>(string teacherId)
        {
            return await this.usersRepository.All()
                .Where(x => x.StudentCourses.Any(c => c.EndDate == null) && x.StudentCourses.Any(c => c.TeacherId == teacherId))
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

        public int GetCount()
        {
            return this.usersRepository.All().Where(x => x.StudentCourses.Any(c => c.EndDate == null)).Count();
        }

        public async Task UploadImagesAsync(StudentPortfolioInputModel input, string imagePath)
        {
            var student = this.usersRepository.All().FirstOrDefault(x => x.Id == input.StudentId);
            Directory.CreateDirectory($"{imagePath}");
            foreach (var image in input.Images)
            {
                var extension = Path.GetExtension(image.FileName).TrimStart('.');
                if (!this.allowedExtensions.Any(x => extension.EndsWith(x)))
                {
                    throw new Exception($"Invalid file extension {extension}");
                }

                var dbImage = new Image
                {
                    ContentType = image.ContentType,
                    Extension = extension,
                };

                student.Images.Add(dbImage);

                var physicalPath = $"{imagePath}/{dbImage.Id}.{extension}";
                using Stream fileStream = new FileStream(physicalPath, FileMode.Create);
                await image.CopyToAsync(fileStream);
            }

            await this.usersRepository.SaveChangesAsync();
        }
    }
}
