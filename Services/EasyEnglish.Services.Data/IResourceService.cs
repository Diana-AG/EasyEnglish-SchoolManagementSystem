namespace EasyEnglish.Services.Data
{
    using System.Threading.Tasks;

    using EasyEnglish.Web.ViewModels.Administration.Resources;

    public interface IResourceService
    {
        Task AddRemoteUrlAsync(ResourceUrlInputModel input);

        Task UploadFileAsync(ResourceUploadFileInputModel input, string imagePath);

        // IEnumerable<T> GetAll<T>(int page, int itemsPerPage = 12);

        // IEnumerable<T> GetRandom<T>(int count);

        int GetCount();

        Task<T> GetByIdAsync<T>(int id);

        // IEnumerable<T> GetByIngredients<T>(IEnumerable<int> ingredientIds);
        ResourceViewModel AllResources();

        Task DeleteAsync(int id);
    }
}
