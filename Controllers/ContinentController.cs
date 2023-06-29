using System;
using AutoMapper;
using CloudDevAPI_DotNet.DTO;
using CloudDevAPI_DotNet.Interfaces;
using CloudDevAPI_DotNet.Models;
using Microsoft.AspNetCore.Mvc;

namespace CloudDevAPI_DotNet.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ContinentController : Controller
	{

		private readonly IContinentRepository _continentRepository;
        private readonly IMapper _mapper;

        public ContinentController(IContinentRepository continentRepository, IMapper mapper)
		{
			_continentRepository = continentRepository;
			_mapper = mapper;
		}

		[HttpGet]
		[ProducesResponseType(200, Type = typeof(IEnumerable<Continent>))]
		public IActionResult GetContinents()
		{
			var continents = _mapper.Map<List<ContinentDto>>(_continentRepository.GetContinents());

			if(!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			return Ok(continents);
		}

        [HttpGet("{continentId}")]
        [ProducesResponseType(200, Type = typeof(Continent))]
        [ProducesResponseType(400)]
        public IActionResult GetContinent(int continentId)
        {
            var continent = _mapper.Map<ContinentDto>(_continentRepository.GetContinent(continentId));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(continent);
        }

		[HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
		public IActionResult CreateContinent([FromBody] ContinentDtoCreate continentCreate)
		{
			if (continentCreate == null)
			{
				return BadRequest(ModelState);
			}

			var continent = _continentRepository.GetContinents()
				.Where(c => c.Name.Trim().ToUpper() == continentCreate.Name.TrimEnd().ToUpper())
				.FirstOrDefault();

			if (continent != null)
			{
				ModelState.AddModelError("", "A continent already exists with the same name.");
				return StatusCode(422, ModelState);
			}

			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			var continentMap = _mapper.Map<Continent>(continentCreate);

			if (!_continentRepository.CreateContinent(continentMap))
			{
				ModelState.AddModelError("", "An error occurred while trying to create this continent.");
				return StatusCode(500, ModelState);
			}

			return Ok("Continent has been successfully created!");
		}

        [HttpPut("{continentId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
		public IActionResult UpdateContinent(int continentId, [FromBody] ContinentDtoCreate updatedContinent)
		{
			if (updatedContinent == null)
			{
				return BadRequest(ModelState);
			}

			if (continentId != updatedContinent.Id)
			{
				return BadRequest(ModelState);
			}

			if (!_continentRepository.Exists(continentId))
			{
				return NotFound();
			}

			if (!ModelState.IsValid)
			{
				return BadRequest();
			}

			var continentMap = _mapper.Map<Continent>(updatedContinent);

			if (!_continentRepository.UpdateContinent(continentMap))
			{
				ModelState.AddModelError("", "An error occurred while trying to update this continent.");
				return StatusCode(500, ModelState);
			}

			return NoContent();
		}

        [HttpDelete("{continentId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
		public IActionResult DeleteContinent(int continentId)
		{
			if (!_continentRepository.Exists(continentId))
			{
				return NotFound();
			}

			var continentToDelete = _continentRepository.GetContinent(continentId);

			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			if (!_continentRepository.DeleteContinent(continentToDelete))
			{
				ModelState.AddModelError("", "An error occurred while trying to delete this continent.");
			}

			return NoContent();
		}
    }
}

