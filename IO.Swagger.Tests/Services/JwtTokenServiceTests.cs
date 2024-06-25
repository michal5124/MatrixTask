using IO.Swagger.Services;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit;

namespace Tests1.Services
{
    public class JwtTokenServiceTests
    {
        private readonly JwtTokenService _jwtTokenService;
        private readonly IConfiguration _configuration;

        public JwtTokenServiceTests()
        {
            // Mock IConfiguration for testing purposes
            var configuration = new Dictionary<string, string>
            {
                { "Jwt:Key", "a72813uhasdkln.sakldSDQDO2181821JXMÇSDHG..123SAKJDa" },
                { "Jwt:ExpirationInMinutes", "60" }
            };

            var config = new ConfigurationBuilder()
                .AddInMemoryCollection(configuration)
                .Build();

            _configuration = config;

            _jwtTokenService = new JwtTokenService(_configuration);
        }

        [Fact]
        public void GenerateToken_ShouldReturnValidToken()
        {
            // Arrange
            var email = "test@example.com";

            // Act
            var token = _jwtTokenService.GenerateToken(email);

            // Assert
            Assert.NotNull(token);
            Assert.NotEmpty(token);
        }

        [Fact]
        public void GenerateToken_ShouldContainEmailClaim()
        {
            // Arrange
            var email = "test@example.com";

            // Act
            var token = _jwtTokenService.GenerateToken(email);
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtToken = tokenHandler.ReadJwtToken(token);
            // Assert
            Assert.Contains(jwtToken.Claims, claim => claim.Type == "email" && claim.Value == email);
        }
    }
}
