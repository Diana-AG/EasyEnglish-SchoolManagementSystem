namespace EasyEnglish.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;

    using EasyEnglish.Data.Common.Repositories;
    using EasyEnglish.Data.Models;

    public class TrainingFormsService : ITrainingFormsService
    {
        private readonly IDeletableEntityRepository<TrainingForm> trainingFormsRepository;

        public TrainingFormsService(IDeletableEntityRepository<TrainingForm> trainingFormsRepository)
        {
            this.trainingFormsRepository = trainingFormsRepository;
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
