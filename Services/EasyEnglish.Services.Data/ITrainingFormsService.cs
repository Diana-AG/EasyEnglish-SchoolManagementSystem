namespace EasyEnglish.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using EasyEnglish.Web.ViewModels.Administration.TrainingForms;

    public interface ITrainingFormsService
    {
        Task DeleteAsync(int id);

        Task<T> GetByIdAsync<T>(int id);

        Task<IEnumerable<T>> GetAllAsync<T>();

        Task CreateAsync(TrainingFormInputModel input);

        Task UpdateAsync(int id, EditTrainingFormInputModel input);

        IEnumerable<KeyValuePair<string, string>> GetAllAsKeyValuePair();
    }
}
