using System;
using CloudDevAPI_DotNet.Models;

namespace CloudDevAPI_DotNet.DTO
{
	public class ContinentDto
	{

        public int Id { get; set; }
        public string Name { get; set; }
        public string Abbreviation { get; set; }
        public ICollection<CountryDtoBones> Country { get; set; }

	}
}

