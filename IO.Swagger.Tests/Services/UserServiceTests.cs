using IO.Swagger.Models;
using IO.Swagger.Repository;
using IO.Swagger.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Tests1.Services
{
    public class UserServiceTests
    {
        private readonly Mock<IUserRepository> _userRepositoryMock;
        private readonly IUserService _userService;

        public UserServiceTests()
        {
            // Setup IUserRepository Mock
            _userRepositoryMock = new Mock<IUserRepository>();
            _userService = new UserService(_userRepositoryMock.Object);
        }

        [Fact]
        public async Task GetUserByEmail_ValidEmail_ReturnsUser()
        {
            // Arrange
            string email = "test@example.com";
            var expectedUser = new User { Email = email };
            _userRepositoryMock.Setup(repo => repo.getUserInfoByEmail(email))
                              .ReturnsAsync(expectedUser);

            // Act
            var result = await _userService.getUserInfoByEmail(email);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedUser.Email, result.Email);
        }

        [Fact]
        public void AddUserInfo_ValidUser_CallsRepositoryAdd()
        {
            // Arrange
            var userToAdd = new User { Email = "test@example.com" };

            // Act
            _userService.addUserInfo(userToAdd);

            // Assert
            _userRepositoryMock.Verify(repo => repo.Add(userToAdd), Times.Once);
        }

        [Fact]
        public void RemoveUserInfo_ValidUser_CallsRepositoryRemove()
        {
            // Arrange
            var userToRemove = new User { Email = "test@example.com" };

            // Act
            _userService.removeUserInfo(userToRemove);

            // Assert
            _userRepositoryMock.Verify(repo => repo.Remove(userToRemove), Times.Once);
        }

        [Fact]
        public void Get_ReturnsListOfUsers()
        {
            // Arrange
            var expectedUsers = new List<User>
            {
                new User {Email = "user1@example.com" },
                new User {Email = "user2@example.com" }
            };
            _userRepositoryMock.Setup(repo => repo.Get())
                              .ReturnsAsync(expectedUsers);

            // Act
            var result = _userService.Get();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedUsers.Count, result.Count);
            Assert.Equal(expectedUsers[1].Email, result[1].Email);
        }
    }
}
