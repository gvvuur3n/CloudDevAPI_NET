using System;
namespace CloudDevAPI_DotNet.DTO.Posts
{
	public class UserDtoCreate
	{
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
    }
}

