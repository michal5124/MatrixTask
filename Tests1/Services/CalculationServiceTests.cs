using IO.Swagger.Models;
using IO.Swagger.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Tests1.Services
{
    public class CalculationServiceTests
    {
        private readonly CalculationService _service;

        public CalculationServiceTests()
        {
            _service = new CalculationService();
        }

        [Fact]
        public void Calculate_AddOperation_ReturnsCorrectSum()
        {
            // Arrange
            var request = new CalculationRequest
            {
                Operand1 = 5,
                Operand2 = 3,
                Operation = OperationEnum.AddEnum
            };

            // Act
            var result = _service.Calculate(request);

            // Assert
            Assert.Equal(8, result);
        }

        [Fact]
        public void Calculate_SubtractOperation_ReturnsCorrectDifference()
        {
            // Arrange
            var request = new CalculationRequest
            {
                Operand1 = 5,
                Operand2 = 3,
                Operation = OperationEnum.SubtractEnum
            };

            // Act
            var result = _service.Calculate(request);

            // Assert
            Assert.Equal(2, result);
        }

        [Fact]
        public void Calculate_MultiplyOperation_ReturnsCorrectProduct()
        {
            // Arrange
            var request = new CalculationRequest
            {
                Operand1 = 5,
                Operand2 = 3,
                Operation = OperationEnum.MultiplyEnum
            };

            // Act
            var result = _service.Calculate(request);

            // Assert
            Assert.Equal(15, result);
        }

        [Fact]
        public void Calculate_DivideOperation_ReturnsCorrectQuotient()
        {
            // Arrange
            var request = new CalculationRequest
            {
                Operand1 = 6,
                Operand2 = 3,
                Operation = OperationEnum.DivideEnum
            };

            // Act
            var result = _service.Calculate(request);

            // Assert
            Assert.Equal(2, result);
        }

        [Fact]
        public void Calculate_InvalidOperation_ThrowsArgumentException()
        {
            // Arrange
            var request = new CalculationRequest
            {
                Operand1 = 6,
                Operand2 = 3,
                Operation = (OperationEnum)99 // Invalid operation
            };

            // Act & Assert
            Assert.Throws<ArgumentException>(() => _service.Calculate(request));
        }
    }
}
