using System;
using System.Diagnostics.Metrics;
using CloudDevAPI_DotNet.Data;
using CloudDevAPI_DotNet.Interfaces;
using CloudDevAPI_DotNet.Models;
using Microsoft.EntityFrameworkCore;

namespace CloudDevAPI_DotNet.Repository
{
	public class UserRepository : IUserRepository
	{

		private readonly DataContext _context;

		public UserRepository(DataContext context)
		{
			_context = context;
		}

		public User GetUser(int id)
		{
			var user = _context.User.Where(p => p.Id == id).Include(w => w.Country).FirstOrDefault();

			return user;
		}

		public User GetUserByUsername(string username)
		{
			return _context.User.Where(p => p.Username == username).Include(w => w.Country).FirstOrDefault();
		}

		public User GetUserByEmail(string email)
		{
			return _context.User.Where(p => p.Email == email).FirstOrDefault();
		}

        public ICollection<User> GetUsers()
        {
            return _context.User.OrderBy(p => p.Id).Include(w => w.Country).ToList();
        }

        public bool CreateUser(User user)
        {
            _context.Add(user);
            return Save();
        }

        public bool UpdateUser(User user)
        {
            _context.Update(user);
            return Save();
        }

        public bool DeleteUser(User user)
        {
            _context.Remove(user);
            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool Exists(int id)
        {
            return _context.User.Any(c => c.Id == id);
        }

        public bool AddCountryToUser(User user, Country country)
        {
            var userToBeEdited = _context.User.Where(p => p.Id == user.Id).Include(w => w.Country).FirstOrDefault();
            userToBeEdited.Country.Add(country);
            return Save();
        }

        public bool RemoveCountryFromUser(User user, Country country)
        {
            var userToBeEdited = _context.User.Where(p => p.Id == user.Id).Include(w => w.Country).FirstOrDefault();
            userToBeEdited.Country.Remove(country);
            return Save();
        }
    }
}

