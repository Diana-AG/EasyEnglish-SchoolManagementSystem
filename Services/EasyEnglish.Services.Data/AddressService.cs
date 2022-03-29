namespace EasyEnglish.Services.Data
{
    using System.Linq;
    using System.Threading.Tasks;

    using EasyEnglish.Data.Common.Repositories;
    using EasyEnglish.Data.Models;

    public class AddressService : IAddressService
    {
        private readonly IDeletableEntityRepository<Country> countriesRepository;
        private readonly IDeletableEntityRepository<Town> townsRepository;
        private readonly IDeletableEntityRepository<Address> addressesRepository;

        public AddressService(
            IDeletableEntityRepository<Country> countriesRepository,
            IDeletableEntityRepository<Town> townsRepository,
            IDeletableEntityRepository<Address> addressesRepository)
        {
            this.countriesRepository = countriesRepository;
            this.townsRepository = townsRepository;
            this.addressesRepository = addressesRepository;
        }

        public async Task<Address> SetAddress(string addressName)
        {
            addressName = addressName.ToUpper();
            var address = this.addressesRepository.AllAsNoTracking().FirstOrDefault(a => a.Name == addressName);

            if (address == null)
            {
                address = new Address { Name = addressName };
                await this.addressesRepository.AddAsync(address);
                await this.addressesRepository.SaveChangesAsync();
            }

            return address;
        }

        public async Task<Country> SetCountry(string countryName)
        {
            countryName = countryName.ToUpper();
            var country = this.countriesRepository.AllAsNoTracking().FirstOrDefault(t => t.Name == countryName);

            if (country == null)
            {
                country = new Country { Name = countryName };
                await this.countriesRepository.AddAsync(country);
                await this.countriesRepository.SaveChangesAsync();
            }

            return country;
        }

        public async Task<Town> SetTown(string townName)
        {
            townName = townName.ToUpper();
            var town = this.townsRepository.AllAsNoTracking().FirstOrDefault(c => c.Name == townName);

            if (town == null)
            {
                town = new Town { Name = townName };
                await this.townsRepository.AddAsync(town);
                await this.townsRepository.SaveChangesAsync();
            }

            return town;
        }
    }
}
