using System;
using CloudDevAPI_DotNet.Interfaces;
using CloudDevAPI_DotNet.Models;
using CloudDevAPI_DotNet.Repository;
using Microsoft.AspNetCore.Mvc;
namespace CloudDevAPI_DotNet.Controllers
{
	[Route("api/[Controller]")]
	[ApiController]
	public class UserController : Controller
	{
		private readonly IUserRepository _userRepository;
		public UserController(IUserRepository userRepository)
		{
			_userRepository = userRepository;
		}

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<User>))]
        public IActionResult GetUsers()
        {
            var users = _userRepository.GetUsers();

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
           
            return Ok(users);
        }

        [HttpGet("{userId:int}")]
		[ProducesResponseType(200, Type = typeof(User))]
		[ProducesResponseType(400)]
		public IActionResult GetUser(int userId)
		{
            var user = _userRepository.GetUser(userId);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(user);
        }
    }
}

