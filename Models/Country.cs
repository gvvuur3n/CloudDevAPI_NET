using System;
namespace CloudDevAPI_DotNet.Models
{
	public class Country
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string FlagURL { get; set; }
		public string Abbreviation { get; set; }
		public string Capital { get; set; }
		public int Population { get; set; }
		public string Description { get; set; }
        public virtual ICollection<User> User { get; set; }
        public virtual Continent Continent { get; set; }
    }
}

