namespace EasyEnglish.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using EasyEnglish.Data.Common.Repositories;
    using EasyEnglish.Data.Models;
    using EasyEnglish.Services.Mapping;
    using EasyEnglish.Web.ViewModels.Administration.TrainingForms;
    using Microsoft.EntityFrameworkCore;

    public class TrainingFormsService : ITrainingFormsService
    {
        private readonly IDeletableEntityRepository<TrainingForm> trainingFormsRepository;

        public TrainingFormsService(IDeletableEntityRepository<TrainingForm> trainingFormsRepository)
        {
            this.trainingFormsRepository = trainingFormsRepository;
        }

        public async Task DeleteAsync(int id)
        {
            var trainingForm = await this.trainingFormsRepository.All().FirstOrDefaultAsync(x => x.Id == id);
            this.trainingFormsRepository.Delete(trainingForm);
            await this.trainingFormsRepository.SaveChangesAsync();
        }

        public async Task<T> GetByIdAsync<T>(int id)
        {
            var trainingForm = await this.trainingFormsRepository.AllAsNoTracking()
                .Where(x => x.Id == id)
                .To<T>().FirstOrDefaultAsync();

            return trainingForm;
        }

        public async Task<IEnumerable<T>> GetAllAsync<T>()
        {
            var trainingForms = await this.trainingFormsRepository.AllAsNoTracking()
                .OrderBy(x => x.Name)
                .To<T>().ToListAsync();

            return trainingForms;
        }

        public async Task CreateAsync(TrainingFormInputModel input)
        {
            var trainingForm = this.trainingFormsRepository.All().FirstOrDefault(x => x.Name == input.Name);
            if (trainingForm != null)
            {
                throw new Exception($"Training Form with name \"{input.Name}\" already exists");
            }

            trainingForm = new TrainingForm { Name = input.Name };

            await this.trainingFormsRepository.AddAsync(trainingForm);
            await this.trainingFormsRepository.SaveChangesAsync();
        }

        public async Task UpdateAsync(int id, EditTrainingFormInputModel input)
        {
            var trainingForm = this.trainingFormsRepository.All().FirstOrDefault(x => x.Id == id);
            trainingForm.Name = input.Name;

            await this.trainingFormsRepository.SaveChangesAsync();
        }

        public IEnumerable<KeyValuePair<string, string>> GetAllAsKeyValuePair()
        {
            return this.trainingFormsRepository.All().Select(x => new
            {
                x.Id,
                x.Name,
            }).ToList().Select(x => new KeyValuePair<string, string>(x.Id.ToString(), x.Name));
        }
    }
}
