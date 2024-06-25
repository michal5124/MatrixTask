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

    public class CalculatorApiControllerTests
    {
        private readonly Mock<ICalculationService> _mockCalculationService;
        private readonly CalculatorApiController _controller;

        public CalculatorApiControllerTests()
        {
            _mockCalculationService = new Mock<ICalculationService>();
            _controller = new CalculatorApiController(_mockCalculationService.Object);
        }

        [Fact]
        public async Task CalculatePost_ValidRequest_ReturnsOk()
        {
            // Arrange
            var calculationRequest = new CalculationRequest { Operand1 = 10, Operand2 = 5 };
            var expectedResult = 15; // This is a mock calculation result
            _mockCalculationService.Setup(x => x.Calculate(calculationRequest))
                                   .Returns(expectedResult);

            // Act
            var result = _controller.CalculatePost(calculationRequest) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
            var calculationResult = Assert.IsType<CalculationResult>(result.Value);
            Assert.Equal(expectedResult, calculationResult.Result);
        }

        [Fact]
        public async Task CalculatePost_ServiceThrowsArgumentException_ReturnsBadRequest()
        {
            // Arrange
            var calculationRequest = new CalculationRequest { Operand1 = 10, Operand2 = 0 };
            _mockCalculationService.Setup(x => x.Calculate(calculationRequest))
                                   .Throws(new ArgumentException("Operand2 cannot be zero"));

            // Act
            var result = _controller.CalculatePost(calculationRequest) as BadRequestObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(400, result.StatusCode);
            var errorResponse = Assert.IsType<ErrorResponse>(result.Value);
            Assert.Equal("Operand2 cannot be zero", errorResponse.Error);
        }

        [Fact]
        public async Task CalculatePost_ServiceThrowsException_ReturnsInternalServerError()
        {
            // Arrange
            var calculationRequest = new CalculationRequest { Operand1 = 10, Operand2 = 5 };
            _mockCalculationService.Setup(x => x.Calculate(calculationRequest))
                                   .Throws(new Exception("An unexpected error occurred"));

            // Act
            var result = _controller.CalculatePost(calculationRequest) as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(500, result.StatusCode);
            var errorResponse = Assert.IsType<ErrorResponse>(result.Value);
            Assert.Equal("An unexpected error occurred", errorResponse.Error);
        }
    }
}
