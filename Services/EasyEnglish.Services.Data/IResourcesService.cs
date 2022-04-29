namespace EasyEnglish.Services.Data
{
    using System.Threading.Tasks;

    using EasyEnglish.Web.ViewModels.Administration.Resources;

    public interface IResourcesService
    {
        Task AddRemoteUrlAsync(ResourceUrlInputModel input);

        Task UploadFileAsync(ResourceUploadFileInputModel input, string imagePath);

        int GetCount();

        Task<T> GetByIdAsync<T>(int id);

        Task<T> GetByNameAsync<T>(string name);

        ResourceViewModel GetAll();

        Task DeleteAsync(int id, string resourcePath);

        bool NameExists(string name);
    }
}
