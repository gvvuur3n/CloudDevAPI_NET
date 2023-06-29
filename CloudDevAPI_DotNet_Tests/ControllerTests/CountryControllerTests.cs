using AutoMapper;
using CloudDevAPI_DotNet.Controllers;
using CloudDevAPI_DotNet.DTO;
using CloudDevAPI_DotNet.Interfaces;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;

namespace CloudDevAPI_DotNet_Tests;

public class CountryControllerTests
{
    private readonly ICountryRepository _countryRepository;
    private readonly IContinentRepository _continentRepository;
    private readonly IMapper _mapper;

    public CountryControllerTests()
    {
        _countryRepository = A.Fake<ICountryRepository>();
        _continentRepository = A.Fake<IContinentRepository>();
        _mapper = A.Fake<IMapper>();
    }

    [Fact]
    public void CountryController_GetCountries_ReturnOK()
    {
        //Arrange
        var countries = A.Fake<ICollection<CountryDto>>();
        var countryList = A.Fake<List<CountryDto>>();

        A.CallTo(() => _mapper.Map<List<CountryDto>>(countries)).Returns(countryList);
        var controller = new CountryController(_countryRepository, _mapper, _continentRepository);

        //Act
        var result = controller.GetCountries();

        //Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(OkObjectResult));
    }

    //[Fact]
    //public void ContinentController_GetContinent_ReturnOk()
    //{
    //    //Arrange
    //    var continents = A.Fake<ICollection<CountryDto>>();
    //    var continentList = A.Fake<List<CountryDto>>();

    //    A.CallTo(() => _mapper.Map<List<CountryDto>>(continents)).Returns(continentList);
    //    var controller = new CountryController(_countryRepository, _mapper);

    //    //Act
    //    var result = controller.GetContinent(1);

    //    //Assert
    //    result.Should().NotBeNull();
    //    result.Should().BeOfType(typeof(OkObjectResult));
    //}
}
