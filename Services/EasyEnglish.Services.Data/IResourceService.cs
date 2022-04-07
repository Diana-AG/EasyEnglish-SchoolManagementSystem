namespace EasyEnglish.Services.Data
{
    using System.Linq;
    using System.Threading.Tasks;
    using EasyEnglish.Data.Models;
    using EasyEnglish.Web.ViewModels.Administration.Resources;

    public interface IResourceService
    {
        Task CreateAsync(ResourceInputModel input);

        //string userId, string imagePath);

        //IEnumerable<T> GetAll<T>(int page, int itemsPerPage = 12);

        //IEnumerable<T> GetRandom<T>(int count);

        //int GetCount();

        //T GetById<T>(int id);

        //Task UpdateAsync(int id, EditRecipeInputModel input);

        //IEnumerable<T> GetByIngredients<T>(IEnumerable<int> ingredientIds);

        IQueryable<ResourceViewModel> AllResources();

        Task DeleteAsync(int id);

        Task<Resource> GetResourceByIdAsync(int id);

        Task<ResourceViewModel> GetResourceViewModelByIdAsync(int id);
    }
}
