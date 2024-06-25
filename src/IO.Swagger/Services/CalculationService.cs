using IO.Swagger.Models;
using Microsoft.AspNetCore.JsonPatch.Operations;
using System;

namespace IO.Swagger.Services
{
    public interface ICalculationService
    {
        decimal? Calculate(CalculationRequest request);
    }

    public class CalculationService : ICalculationService
    {
        public decimal? Calculate(CalculationRequest request)
        {
            return request.Operation switch
            {
                OperationEnum.AddEnum => request.Operand1 + request.Operand2,
                OperationEnum.SubtractEnum => request.Operand1 - request.Operand2,
                OperationEnum.MultiplyEnum => request.Operand1 * request.Operand2,
                OperationEnum.DivideEnum => request.Operand1 / request.Operand2,
                _ => throw new ArgumentException("Invalid operator")
            };
        }
    }
}
