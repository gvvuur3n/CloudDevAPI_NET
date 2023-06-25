using System;
using CloudDevAPI_DotNet.Data;
using CloudDevAPI_DotNet.Interfaces;
using CloudDevAPI_DotNet.Models;

namespace CloudDevAPI_DotNet.Repository
{
	public class CountryRepository : ICountryRepository
	{
        private readonly DataContext _context;

        public CountryRepository(DataContext context)
		{
			_context = context;
		}

		public ICollection<Country> GetCountries()
		{
			return _context.Country.OrderBy(p => p.Id).ToList();
		}

		public Country GetCountry(int id)
		{
			return _context.Country.Where(p => p.Id == id).FirstOrDefault();
		}

		public Country GetCountry(string name)
		{
            return _context.Country.Where(p => p.Name == name).FirstOrDefault();
        }

		public int GetCountryPopulationById(int id)
		{
			throw new NotImplementedException();
		}
	}
}

