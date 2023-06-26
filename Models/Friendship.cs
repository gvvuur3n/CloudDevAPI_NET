using System;
using System.ComponentModel.DataAnnotations;

namespace CloudDevAPI_DotNet.Models
{
	public class Friendship
	{
		[Key]
		public int UserId { get; set; }
		public int FriendId { get; set; }
	}
}

