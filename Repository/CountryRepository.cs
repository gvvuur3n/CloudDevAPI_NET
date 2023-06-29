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

        public bool CreateCountry(Country country)
        {
            _context.Add(country);
            return Save();
        }

        public bool DeleteCountry(Country country)
        {
            _context.Remove(country);
            return Save();
        }

        public bool Exists(int id)
        {
            return _context.Country.Any(c => c.Id == id);
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
            return _context.Country.Where(p => p.Id == id).FirstOrDefault().Population;
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateCountry(Country country)
        {
            _context.Update(country);
            return Save();
        }
    }
}

