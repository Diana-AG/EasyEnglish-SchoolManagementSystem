namespace EasyEnglish.Services.Data
{
    using System.Threading.Tasks;

    using EasyEnglish.Web.ViewModels.Administration.Courses;

    public interface IResourceService
    {
        Task CreateAsync(CreateResourceInputModel input);
            //, string userId, string imagePath);

        //IEnumerable<T> GetAll<T>(int page, int itemsPerPage = 12);

        //IEnumerable<T> GetRandom<T>(int count);

        //int GetCount();

        //T GetById<T>(int id);

        //Task UpdateAsync(int id, EditRecipeInputModel input);

        //IEnumerable<T> GetByIngredients<T>(IEnumerable<int> ingredientIds);

        //Task DeleteAsync(int id);
    }
}
