using System;
using CloudDevAPI_DotNet.Data;
using CloudDevAPI_DotNet.Interfaces;
using CloudDevAPI_DotNet.Models;

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
			return _context.User.Where(p => p.Id == id).FirstOrDefault();
		}

		public User GetUserByUsername(string username)
		{
			return _context.User.Where(p => p.Username == username).FirstOrDefault();
		}

		public User GetUserByEmail(string email)
		{
			return _context.User.Where(p => p.Email == email).FirstOrDefault();
		}

        public ICollection<User> GetUsers()
        {
            return _context.User.OrderBy(p => p.Id).ToList();
        }
    }
}

