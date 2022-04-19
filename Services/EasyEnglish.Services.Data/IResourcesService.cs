namespace EasyEnglish.Services.Data
{
    using System.Threading.Tasks;

    using EasyEnglish.Web.ViewModels.Administration.Resources;

    public interface IResourcesService
    {
        Task AddRemoteUrlAsync(ResourceUrlInputModel input);

        Task UploadFileAsync(ResourceUploadFileInputModel input, string imagePath);

        // IEnumerable<T> GetAll<T>(int page, int itemsPerPage = 12);

        // IEnumerable<T> GetRandom<T>(int count);

        int GetCount();

        Task<T> GetByIdAsync<T>(int id);

        Task<T> GetByNameAsync<T>(string name);

        // IEnumerable<T> GetByIngredients<T>(IEnumerable<int> ingredientIds);
        ResourceViewModel GetAll();

        Task DeleteAsync(int id);

        bool NameExists(string name);
    }
}
