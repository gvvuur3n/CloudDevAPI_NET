using System;
using AutoMapper;
using CloudDevAPI_DotNet.DTO;
using CloudDevAPI_DotNet.DTO.Posts;
using CloudDevAPI_DotNet.Models;

namespace CloudDevAPI_DotNet.Helper
{
	public class MappingProfiles : Profile
	{
		public MappingProfiles()
		{
			CreateMap<Continent, ContinentDto>();
			CreateMap<ContinentDtoCreate, Continent>();
			CreateMap<Country, CountryDto>();
			CreateMap<Country, CountryDtoBones>();
			CreateMap<CountryDtoCreate, Country>();
			CreateMap<User, UserDto>();
			CreateMap<UserDtoCreate, User>();
		}
	}
}

