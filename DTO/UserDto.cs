﻿using System;
using CloudDevAPI_DotNet.Models;

namespace CloudDevAPI_DotNet.DTO
{
	public class UserDto
	{
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        //public ICollection<Country> Countries { get; set; }
        //public ICollection<Friendship> Friendships { get; set; }
    }
}

