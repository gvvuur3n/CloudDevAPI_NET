using System;
using AutoMapper;
using CloudDevAPI_DotNet.DTO;
using CloudDevAPI_DotNet.DTO.Posts;
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
        private readonly IMapper _mapper;
        private readonly ICountryRepository _countryRepository;

		public UserController(IUserRepository userRepository, IMapper mapper, ICountryRepository countryRepository)
		{
			_userRepository = userRepository;
            _mapper = mapper;
            _countryRepository = countryRepository;
		}

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<User>))]
        public IActionResult GetUsers()
        {
            var users = _mapper.Map<List<UserDto>>(_userRepository.GetUsers());

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
            var user = _mapper.Map<UserDto>(_userRepository.GetUser(userId));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(user);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateUser([FromBody] UserDtoCreate userCreate)
        {
            if (userCreate == null)
            {
                return BadRequest(ModelState);
            }

            var user = _userRepository.GetUsers()
                .Where(c => c.Username.Trim().ToUpper() == userCreate.Username.TrimEnd().ToUpper())
                .FirstOrDefault();

            if (user != null)
            {
                ModelState.AddModelError("", "A user already exists with the same username.");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userMap = _mapper.Map<User>(userCreate);

            if (!_userRepository.CreateUser(userMap))
            {
                ModelState.AddModelError("", "An error occurred while trying to create this user.");
                return StatusCode(500, ModelState);
            }

            return Ok("User has been successfully created!");
        }

        [HttpPut("{userId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateUser(int userId, [FromBody] UserDtoCreate updatedUser)
        {
            if (updatedUser == null)
            {
                return BadRequest(ModelState);
            }

            if (userId != updatedUser.Id)
            {
                return BadRequest(ModelState);
            }

            if (!_userRepository.Exists(userId))
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var userMap = _mapper.Map<User>(updatedUser);

            if (!_userRepository.UpdateUser(userMap))
            {
                ModelState.AddModelError("", "An error occurred while trying to update this user.");
                return StatusCode(500, ModelState);
            }

            return Ok("Success");
        }

        [HttpPut("{userId}/addCountryToUser")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult AddUserCountry(int userId, int countryId)
        { 
            if (!_userRepository.Exists(userId))
            {
                return NotFound();
            }

            if (!_countryRepository.Exists(countryId))
            {
                ModelState.AddModelError("","The country in question does not exist and therefore cannot be added to this user.");
                return StatusCode(422, ModelState);
            }

            var userToAddCountryTo = _userRepository.GetUser(userId);
            var countryToAddToUser = _countryRepository.GetCountry(countryId);

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var userMap = _mapper.Map<User>(userToAddCountryTo);

            if (!_userRepository.AddCountryToUser(userMap,countryToAddToUser))
            {
                ModelState.AddModelError("", "An error occurred while trying to update this user.");
                return StatusCode(500, ModelState);
            }

            return Ok("Success");
        }

        [HttpPut("{userId}/removeCountryFromUser")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult RemoveUserCountry(int userId, int countryId)
        {
            if (!_userRepository.Exists(userId))
            {
                return NotFound();
            }

            if (!_countryRepository.Exists(countryId))
            {
                ModelState.AddModelError("", "The country in question does not exist and therefore cannot be added to this user.");
                return StatusCode(422, ModelState);
            }

            var userToRemoveCountryFrom = _userRepository.GetUser(userId);
            var countryToRemoveFromUser = _countryRepository.GetCountry(countryId);

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var userMap = _mapper.Map<User>(userToRemoveCountryFrom);

            if (!_userRepository.RemoveCountryFromUser(userMap, countryToRemoveFromUser))
            {
                ModelState.AddModelError("", "An error occurred while trying to update this user.");
                return StatusCode(500, ModelState);
            }

            return Ok("Success");
        }

        [HttpDelete("{userId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteUser(int userId)
        {
            if (!_userRepository.Exists(userId))
            {
                return NotFound();
            }

            var userToDelete = _userRepository.GetUser(userId);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!_userRepository.DeleteUser(userToDelete))
            {
                ModelState.AddModelError("", "An error occurred while trying to delete this user.");
            }

            return Ok("Success");
        }
    }
}

