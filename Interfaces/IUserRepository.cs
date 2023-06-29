using System;
using CloudDevAPI_DotNet.Models;

namespace CloudDevAPI_DotNet.Interfaces
{
	public interface IUserRepository
	{
        ICollection<User> GetUsers();
		User GetUser(int id);
		User GetUserByUsername(string username);
		User GetUserByEmail(string email);
        bool CreateUser(User user);
        bool UpdateUser(User user);
        bool AddCountryToUser(User user, Country country);
        bool RemoveCountryFromUser(User user, Country country);
        bool DeleteUser(User user);
        bool Save();
        bool Exists(int userId);

    }
}

