using System;
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

		public ContinentController(IContinentRepository continentRepository)
		{
			_continentRepository = continentRepository;
		}

		[HttpGet]
		[ProducesResponseType(200, Type = typeof(IEnumerable<Continent>))]
		public IActionResult GetContinents()
		{
			var continents = _continentRepository.GetContinents();

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
            var continent = _continentRepository.GetContinent(continentId);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(continent);
        }
    }
}

