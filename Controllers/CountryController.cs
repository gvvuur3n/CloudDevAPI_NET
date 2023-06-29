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
	[Route("api/[controller]")]
	[ApiController]
	public class CountryController : Controller
	{
        private readonly ICountryRepository _countryRepository;
        private readonly IMapper _mapper;
        private readonly IContinentRepository _continentRepository;

        public CountryController(ICountryRepository countryRepository, IMapper mapper, IContinentRepository continentRepository)
		{
            _countryRepository = countryRepository;
            _continentRepository = continentRepository;
            _mapper = mapper;
        }

		[HttpGet]
		[ProducesResponseType(200, Type = typeof(IEnumerable<Country>))]
		public IActionResult GetCountries()
		{
			var countries = _mapper.Map<List<CountryDto>>(_countryRepository.GetCountries());

			if(!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			return Ok(countries);
		}

		[HttpGet("{countryId}")]
		[ProducesResponseType(200, Type = typeof(Country))]
		[ProducesResponseType(400)]
		public IActionResult GetCountry(int countryId)
		{
			var country = _mapper.Map<CountryDto>(_countryRepository.GetCountry(countryId));

			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			return Ok(country);
		}

        [HttpGet("{countryId}/getCountryPopulationById")]
        [ProducesResponseType(200, Type = typeof(int))]
        [ProducesResponseType(400)]
        public IActionResult GetCountryPopulationById(int countryId)
        {
            var countryPopulation = _countryRepository.GetCountryPopulationById(countryId);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(countryPopulation);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateCountry([FromBody] CountryDtoCreate countryCreate, [FromQuery] int continentId)
        {
            if (countryCreate == null)
            {
                return BadRequest(ModelState);
            }

            var country = _countryRepository.GetCountries()
                .Where(c => c.Name.Trim().ToUpper() == countryCreate.Name.TrimEnd().ToUpper())
                .FirstOrDefault();

            if (country != null)
            {
                ModelState.AddModelError("", "A country already exists with the same name.");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var countryMap = _mapper.Map<Country>(countryCreate);
            countryMap.Continent = _continentRepository.GetContinent(continentId);

            if (!_countryRepository.CreateCountry(countryMap))
            {
                ModelState.AddModelError("", "An error occurred while trying to create this country.");
                return StatusCode(500, ModelState);
            }

            return Ok("Country has been successfully created!");
        }

        [HttpPut("{countryId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateCountry(int countryId, [FromBody]  CountryDtoCreate updatedCountry, [FromQuery] int continentId)
        {
            if (updatedCountry == null)
            {
                return BadRequest(ModelState);
            }

            if (countryId != updatedCountry.Id)
            {
                return BadRequest(ModelState);
            }

            if (!_countryRepository.Exists(countryId))
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var countryMap = _mapper.Map<Country>(updatedCountry);

            countryMap.Continent = _continentRepository.GetContinent(continentId);

            if (!_countryRepository.UpdateCountry(countryMap))
            {
                ModelState.AddModelError("", "An error occurred while trying to update this country.");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("{countryId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteCountry(int countryId)
        {
            if (!_countryRepository.Exists(countryId))
            {
                return NotFound();
            }

            var countryToDelete = _countryRepository.GetCountry(countryId);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!_countryRepository.DeleteCountry(countryToDelete))
            {
                ModelState.AddModelError("", "An error occurred while trying to delete this country.");
            }

            return NoContent();
        }
    }
}

