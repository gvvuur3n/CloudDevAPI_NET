using System;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Xunit;
using AutoMapper;
using CloudDevAPI_DotNet.Controllers;
using CloudDevAPI_DotNet.Interfaces;
using CloudDevAPI_DotNet.Repository;
using CloudDevAPI_DotNet.DTO;

namespace CloudDevAPI_DotNet_Tests;


public class ContinentControllertests
{ 
    private readonly IContinentRepository _continentRepository;
    private readonly IMapper _mapper;

    public ContinentControllertests()
    {
        _continentRepository = A.Fake<IContinentRepository>();
        _mapper = A.Fake<IMapper>();
    }

    [Fact]
    public void ContinentController_GetContinents_ReturnOK()
    {
        //Arrange
        var continents = A.Fake<ICollection<ContinentDto>>();
        var continentList = A.Fake<List<ContinentDto>>();

        A.CallTo(() => _mapper.Map<List<ContinentDto>>(continents)).Returns(continentList);
        var controller = new ContinentController(_continentRepository, _mapper);

        //Act
        var result = controller.GetContinents();

        //Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(OkObjectResult));
    }

    //[Fact]
    //public void ContinentController_GetContinent_ReturnOk()
    //{
    //    //Arrange
    //    var continents = A.Fake<ICollection<ContinentDto>>();
    //    var continentList = A.Fake<List<ContinentDto>>();

    //    A.CallTo(() => _mapper.Map<List<ContinentDto>>(continents)).Returns(continentList);
    //    var controller = new ContinentController(_continentRepository, _mapper);

    //    //Act
    //    var result = controller.GetContinent(1);

    //    //Assert
    //    result.Should().NotBeNull();
    //    result.Should().BeOfType(typeof(OkObjectResult));
    //}
    
}
