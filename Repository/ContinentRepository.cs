using System;
using CloudDevAPI_DotNet.Data;
using CloudDevAPI_DotNet.Interfaces;
using CloudDevAPI_DotNet.Models;
namespace CloudDevAPI_DotNet.Repository
{
	public class ContinentRepository : IContinentRepository
	{
		private readonly DataContext _context;

		public ContinentRepository(DataContext context)
		{
            _context = context;
		}

        public Continent GetContinent(int id)
        {
            return _context.Continent.Where(p => p.Id == id).FirstOrDefault();
        }

        public ICollection<Continent> GetContinents()
        {
            return _context.Continent.OrderBy(p => p.Id).ToList();
        }
    }
}

