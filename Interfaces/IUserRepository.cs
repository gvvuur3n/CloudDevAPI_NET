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

	}
}

