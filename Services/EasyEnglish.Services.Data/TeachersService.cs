namespace EasyEnglish.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;

    using EasyEnglish.Data.Common.Repositories;
    using EasyEnglish.Data.Models;

    public class TeachersService : ITeachersService
    {
        private readonly IDeletableEntityRepository<ApplicationUser> teachersRepository;

        public TeachersService(IDeletableEntityRepository<ApplicationUser> teachersRepository)
        {
            this.teachersRepository = teachersRepository;
        }

        public IEnumerable<KeyValuePair<string, string>> GetAllAsKeyValuePair()
        {
            return this.teachersRepository.All()
                .OrderBy(x => x.FullName)
                .Select(x => new
                {
                    x.Id,
                    x.FullName,
                }).ToList().Select(x => new KeyValuePair<string, string>(x.Id, x.FullName));
        }
    }
}
