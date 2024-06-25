/*
 * Secure Calculator API
 *
 * No description provided (generated by Swagger Codegen https://github.com/swagger-api/swagger-codegen)
 *
 * OpenAPI spec version: 1.0.0
 * 
 * Generated by: https://github.com/swagger-api/swagger-codegen.git
 */
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.SwaggerGen;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using IO.Swagger.Attributes;
using IO.Swagger.Security;
using Microsoft.AspNetCore.Authorization;
using IO.Swagger.Models;
using IO.Swagger.Services;

namespace IO.Swagger.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [ApiController]
    public class CalculatorApiController : ControllerBase
    {
        private readonly ICalculationService _calculationService;

        public CalculatorApiController(ICalculationService calculationService)
        {
            _calculationService = calculationService;
        }

        /// <summary>
        /// Perform a calculation
        /// </summary>
        /// <param name="body">Calculation parameters</param>
        /// <response code="200">Calculation result</response>
        /// <response code="400">Bad Request</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="500">Internal Server Error</response>
        [HttpPost]
        [Route("/calculate")]
        [Authorize(AuthenticationSchemes = BearerAuthenticationHandler.SchemeName)]
        [ValidateModelState]
        [SwaggerOperation("CalculatePost")]
        [SwaggerResponse(statusCode: 200, type: typeof(CalculationResult), description: "Calculation result")]
        [SwaggerResponse(statusCode: 400, type: typeof(ErrorResponse), description: "Bad Request")]
        [SwaggerResponse(statusCode: 401, type: typeof(ErrorResponse), description: "Unauthorized")]
        [SwaggerResponse(statusCode: 500, type: typeof(ErrorResponse), description: "Internal Server Error")]
        public virtual IActionResult CalculatePost([FromBody] CalculationRequest body)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var result = _calculationService.Calculate(body);
                return Ok(new CalculationResult { Result = result });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new ErrorResponse { Error = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ErrorResponse { Error = ex.Message });
            }
        }
    }
}
