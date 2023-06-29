using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using CloudDevAPI_DotNet.Controllers;
using CloudDevAPI_DotNet.DTO;
using CloudDevAPI_DotNet.DTO.Posts;
using CloudDevAPI_DotNet.Interfaces;
using CloudDevAPI_DotNet.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace CloudDevAPI_DotNet_Tests;

    public class UserControllerTests
    {
        private readonly UserController _controller;
        private readonly Mock<IUserRepository> _userRepositoryMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<ICountryRepository> _countryRepositoryMock;

        public UserControllerTests()
        {
            _userRepositoryMock = new Mock<IUserRepository>();
            _mapperMock = new Mock<IMapper>();
            _countryRepositoryMock = new Mock<ICountryRepository>();

            _controller = new UserController(
                _userRepositoryMock.Object,
                _mapperMock.Object,
                _countryRepositoryMock.Object
            );
        }

        [Fact]
        public void GetUsers_ReturnsOkResultWithUsers()
        {
            // Arrange
            var users = new List<User> { new User { Id = 1, Username = "John" }, new User { Id = 2, Username = "Jane" } };
            var userDtos = new List<UserDto> { new UserDto { Id = 1, Username = "John" }, new UserDto { Id = 2, Username = "Jane" } };

            _userRepositoryMock.Setup(repo => repo.GetUsers()).Returns(users);
            _mapperMock.Setup(mapper => mapper.Map<List<UserDto>>(users)).Returns(userDtos);

            // Act
            var result = _controller.GetUsers();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedUsers = Assert.IsAssignableFrom<IEnumerable<UserDto>>(okResult.Value);
            Assert.Equal(userDtos.Count, returnedUsers.Count());
        }

        [Fact]
        public void GetUser_WithValidId_ReturnsOkResultWithUser()
        {
            // Arrange
            var userId = 1;
            var user = new User { Id = userId, Username = "John" };
            var userDto = new UserDto { Id = userId, Username = "John" };

            _userRepositoryMock.Setup(repo => repo.GetUser(userId)).Returns(user);
            _mapperMock.Setup(mapper => mapper.Map<UserDto>(user)).Returns(userDto);

            // Act
            var result = _controller.GetUser(userId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedUser = Assert.IsType<UserDto>(okResult.Value);
            Assert.Equal(userId, returnedUser.Id);
            Assert.Equal(userDto.Username, returnedUser.Username);
        }

        [Fact]
        public void CreateUser_WithValidUserDtoCreate_ReturnsOkResult()
        {
            // Arrange
            var userCreate = new UserDtoCreate { Id = 1, Username = "John" };

            _userRepositoryMock.Setup(repo => repo.GetUsers()).Returns(new List<User>());
            _userRepositoryMock.Setup(repo => repo.CreateUser(It.IsAny<User>())).Returns(true);
            _mapperMock.Setup(mapper => mapper.Map<User>(userCreate)).Returns(new User());

            // Act
            var result = _controller.CreateUser(userCreate);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal("User has been successfully created!", okResult.Value);
        }

        [Fact]
        public void CreateUser_WithNullUserDtoCreate_ReturnsBadRequestResult()
        {
            // Act
            var result = _controller.CreateUser(null);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal(400, badRequestResult.StatusCode);
        }

        [Fact]
        public void CreateUser_WithExistingUsername_ReturnsUnprocessableEntityResult()
        {
            // Arrange
            var userCreate = new UserDtoCreate { Id = 1, Username = "john" };
            var existingUser = new User { Id = 2, Username = "john" };

            _userRepositoryMock.Setup(repo => repo.GetUsers()).Returns(new List<User> { existingUser });

            // Act
            var result = _controller.CreateUser(userCreate);

            // Assert
            var unprocessableEntityResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(422, unprocessableEntityResult.StatusCode);
        }

        [Fact]
        public void UpdateUser_WithValidUserIdAndUserDtoCreate_ReturnsNoContentResult()
        {
            // Arrange
            var userId = 1;
            var updatedUser = new UserDtoCreate { Id = userId, Username = "John" };
            var existingUser = new User { Id = userId };

            _userRepositoryMock.Setup(repo => repo.Exists(userId)).Returns(true);
            _userRepositoryMock.Setup(repo => repo.UpdateUser(It.IsAny<User>())).Returns(true);
            _mapperMock.Setup(mapper => mapper.Map<User>(updatedUser)).Returns(existingUser);

            // Act
            var result = _controller.UpdateUser(userId, updatedUser);

            // Assert
            var noContentResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(200, noContentResult.StatusCode);
        }

        [Fact]
        public void UpdateUser_WithNullUserDtoCreate_ReturnsBadRequestResult()
        {
            // Arrange
            var userId = 1;

            // Act
            var result = _controller.UpdateUser(userId, null);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal(400, badRequestResult.StatusCode);
        }

        [Fact]
        public void UpdateUser_WithMismatchedUserId_ReturnsBadRequestResult()
        {
            // Arrange
            var userId = 1;
            var updatedUser = new UserDtoCreate { Id = 2 };

            // Act
            var result = _controller.UpdateUser(userId, updatedUser);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal(400, badRequestResult.StatusCode);
        }

        [Fact]
        public void UpdateUser_WithNonExistingUserId_ReturnsNotFoundResult()
        {
            // Arrange
            var userId = 1;
            var updatedUser = new UserDtoCreate { Id = userId };

            _userRepositoryMock.Setup(repo => repo.Exists(userId)).Returns(false);

            // Act
            var result = _controller.UpdateUser(userId, updatedUser);

            // Assert
            var notFoundResult = Assert.IsType<NotFoundResult>(result);
            Assert.Equal(404, notFoundResult.StatusCode);
        }

        [Fact]
        public void AddUserCountry_WithNonExistingUserId_ReturnsNotFoundResult()
        {
            // Arrange
            var userId = 1;
            var countryId = 1;

            _userRepositoryMock.Setup(repo => repo.Exists(userId)).Returns(false);

            // Act
            var result = _controller.AddUserCountry(userId, countryId);

            // Assert
            var notFoundResult = Assert.IsType<NotFoundResult>(result);
            Assert.Equal(404, notFoundResult.StatusCode);
        }

        [Fact]
        public void RemoveUserCountry_WithNonExistingUserId_ReturnsNotFoundResult()
        {
            // Arrange
            var userId = 1;
            var countryId = 1;

            _userRepositoryMock.Setup(repo => repo.Exists(userId)).Returns(false);

            // Act
            var result = _controller.RemoveUserCountry(userId, countryId);

            // Assert
            var notFoundResult = Assert.IsType<NotFoundResult>(result);
            Assert.Equal(404, notFoundResult.StatusCode);
        }
    }

