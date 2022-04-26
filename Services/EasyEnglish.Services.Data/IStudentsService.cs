namespace EasyEnglish.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IStudentsService
    {
        Task<IEnumerable<T>> GetAllActive<T>();

        Task<IEnumerable<T>> GetAllInCourse<T>(int courseId);

        Task<IEnumerable<string>> GetAllEmailsAsync();
    }
}
