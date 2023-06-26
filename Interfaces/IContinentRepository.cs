using System;
using CloudDevAPI_DotNet.Models;
namespace CloudDevAPI_DotNet.Interfaces

{
	public interface IContinentRepository
	{
        ICollection<Continent> GetContinents();
        Continent GetContinent(int id);
	}
}

