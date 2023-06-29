using System;
using CloudDevAPI_DotNet.Models;

namespace CloudDevAPI_DotNet.DTO.Posts
{
	public class CountryDtoCreate
	{
        public int Id { get; set; }
        public string Name { get; set; }
        public string FlagURL { get; set; }
        public string Abbreviation { get; set; }
        public string Capital { get; set; }
        public int Population { get; set; }
        public string Description { get; set; }
    }
}

