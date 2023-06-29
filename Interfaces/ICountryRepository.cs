using System;
using CloudDevAPI_DotNet.Models;

namespace CloudDevAPI_DotNet.Interfaces
{
	public interface ICountryRepository
	{
		ICollection<Country> GetCountries();
		Country GetCountry(int id);
		Country GetCountry(string name);
		int GetCountryPopulationById(int id);
        bool CreateCountry(Country country);
        bool UpdateCountry(Country country);
        bool DeleteCountry(Country country);
        bool Save();
        bool Exists(int countryId);
    }
}

