using IO.Swagger.Controllers;
using IO.Swagger.Models;
using IO.Swagger.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Tests1.Controllers
{
    public class AuthenticationApiControllerTests
    {
        private readonly Mock<IUserService> _mockUserService;
        private readonly Mock<IJwtTokenService> _mockJwtTokenService;
        private readonly AuthenticationApiController _controller;

        public AuthenticationApiControllerTests()
        {
            _mockUserService = new Mock<IUserService>();
            _mockJwtTokenService = new Mock<IJwtTokenService>();

            _controller = new AuthenticationApiController(
                _mockUserService.Object,
                _mockJwtTokenService.Object
            );
        }

        [Fact]
        public async Task AuthLoginPost_ValidCredentials_ReturnsJwtToken()
        {
            // Arrange
            var userEmail = "test@example.com";
            var user = new User { Email = userEmail };
            _mockUserService.Setup(x => x.getUserInfoByEmail(userEmail))
                            .ReturnsAsync(user);

            var jwtToken = "mockJwtToken";
            _mockJwtTokenService.Setup(x => x.GenerateToken(userEmail))
                                .Returns(jwtToken);

            // Act
            var result = _controller.AuthLoginPost(user) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
            Assert.Equal(jwtToken, result.Value);
        }

        [Fact]
        public async Task AuthLoginPost_UserNotFound_ReturnsBadRequest()
        {
            // Arrange
            var userEmail = "nonexistent@example.com";
            _mockUserService.Setup(x => x.getUserInfoByEmail(userEmail))
                            .ReturnsAsync((User)null);

            // Act
            var result = _controller.AuthLoginPost(new User { Email = userEmail }) as BadRequestObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(400, result.StatusCode);
            Assert.Equal("no such email in the system, please register", result.Value);
        }
        [Fact]
        public async Task AuthLogoutDelete_ExistingUser_DeletesUser()
        {
            // Arrange
            var userEmail = "existinguser@example.com";
            _mockUserService.Setup(x => x.getUserInfoByEmail(userEmail))
                            .ReturnsAsync(new User { Email = userEmail });

            // Act
            var result = _controller.AuthLogoutDelete(userEmail) as NoContentResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(204, result.StatusCode);
            _mockUserService.Verify(x => x.removeUserInfo(It.IsAny<User>()), Times.Once);
        }

        [Fact]
        public async Task AuthLogoutDelete_NonExistingUser_ReturnsNotFound()
        {
            // Arrange
            var userEmail = "nonexistentuser@example.com";
            _mockUserService.Setup(x => x.getUserInfoByEmail(userEmail))
                            .ReturnsAsync((User)null);

            // Act
            var result = _controller.AuthLogoutDelete(userEmail) as NotFoundObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(404, result.StatusCode);
        }

        // Add more tests for other scenarios such as error handling

        [Fact]
        public async Task AuthRegisterPost_NewUser_CreatesUser()
        {
            // Arrange
            var newUser = new User { Email = "newuser@example.com" };
            _mockUserService.Setup(x => x.getUserInfoByEmail(newUser.Email))
                            .ReturnsAsync((User)null); // User does not exist

            // Act
            var result = _controller.AuthRegisterPost(newUser) as CreatedAtActionResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(201, result.StatusCode);
            Assert.Equal(nameof(AuthenticationApiController.AuthRegisterPost), result.ActionName);
            Assert.Equal(newUser.Email, result.RouteValues["email"]);
            _mockUserService.Verify(x => x.addUserInfo(newUser), Times.Once);
        }
    }
}
