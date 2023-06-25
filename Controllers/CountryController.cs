﻿using System;
using CloudDevAPI_DotNet.Interfaces;
using CloudDevAPI_DotNet.Models;
using Microsoft.AspNetCore.Mvc;

namespace CloudDevAPI_DotNet.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CountryController : Controller
	{
        private readonly ICountryRepository _countryRepository;

        public CountryController(ICountryRepository countryRepository)
		{
            _countryRepository = countryRepository;
        }

		[HttpGet]
		[ProducesResponseType(200, Type = typeof(IEnumerable<Country>))]
		public IActionResult GetCountries()
		{
			var countries = _countryRepository.GetCountries();

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
			var country = _countryRepository.GetCountry(countryId);

			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			return Ok(country);
		}

	}
}

