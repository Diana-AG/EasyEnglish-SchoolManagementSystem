﻿namespace EasyEnglish.Services.Data
{
    using System.Threading.Tasks;

    using EasyEnglish.Data.Models;

    public interface IAddressesService
    {
        Task<Country> SetCountry(string countryName);

        Task<Town> SetTown(string townName);

        Task<Address> SetAddress(string addressName);
    }
}
