using System;
using CloudDevAPI_DotNet.DTO;
using CloudDevAPI_DotNet.Models;
namespace CloudDevAPI_DotNet.Interfaces

{
	public interface IContinentRepository
	{
        ICollection<Continent> GetContinents();
        Continent GetContinent(int id);
		bool CreateContinent(Continent continent);
		bool UpdateContinent(Continent continent);
		bool DeleteContinent(Continent continent);
		bool Save();
		bool Exists(int continentId);
	}
}

