namespace EasyEnglish.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using EasyEnglish.Web.ViewModels.Administration.Students;

    public interface IStudentsService
    {
        Task<IEnumerable<T>> GetAllActive<T>();

        Task<IEnumerable<T>> GetAllActive<T>(int page, int itemsPerPage = 8);

        Task<IEnumerable<T>> GetAllInCourse<T>(int courseId);

        Task<IEnumerable<T>> GetAllByTeacher<T>(string teacherId);

        Task<IEnumerable<string>> GetAllEmailsAsync();

        Task UploadImagesAsync(StudentPortfolioInputModel input, string imagePath);

        int GetCount();
    }
}
