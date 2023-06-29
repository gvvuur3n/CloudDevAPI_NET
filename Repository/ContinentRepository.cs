using System;
using CloudDevAPI_DotNet.Data;
using CloudDevAPI_DotNet.DTO;
using CloudDevAPI_DotNet.Interfaces;
using CloudDevAPI_DotNet.Models;
using Microsoft.EntityFrameworkCore;

namespace CloudDevAPI_DotNet.Repository
{
	public class ContinentRepository : IContinentRepository
	{
		private readonly DataContext _context;

		public ContinentRepository(DataContext context)
		{
            _context = context;
		}

        public bool CreateContinent(Continent continent)
        {
            _context.Add(continent);
            return Save();
        }

        public bool DeleteContinent(Continent continent)
        {
            _context.Remove(continent);
            return Save();
        }

        public Continent GetContinent(int id)
        {
            return _context.Continent.Where(p => p.Id == id).Include(w => w.Country).FirstOrDefault();
        }

        public ICollection<Continent> GetContinents()
        {
            return _context.Continent.OrderBy(p => p.Id).Include(w => w.Country).ToList();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool Exists(int id)
        {
            return _context.Continent.Any(c => c.Id == id);
        }

        public bool UpdateContinent(Continent continent)
        {
            _context.Update(continent);
            return Save();
        }
    }
}

