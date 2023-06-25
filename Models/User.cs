
using System;
namespace CloudDevAPI_DotNet.Models
{
	public class User
	{
        public int Id { get; set; }
		public string Username { get; set; }
		public string Password { get; set; }
		public string Email { get; set; }
		public ICollection<Country> Countries { get; set; }
		public ICollection<Friendship> Friendships { get; set; }
	}
}

