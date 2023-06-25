using System;
namespace CloudDevAPI_DotNet.Models
{
	public class Continent
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Abbreviation { get; set; }
		public ICollection<Country> Countries { get; set; }
	}
}

