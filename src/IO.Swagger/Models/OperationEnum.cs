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
using System.Linq;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace IO.Swagger.Models
{
  
          /// <summary>
          /// Gets or Sets OperationEnum
          /// </summary>
          [JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
          public enum OperationEnum
          {
              /// <summary>
              /// Enum AddEnum for add
              /// </summary>
              [EnumMember(Value = "add")]
              AddEnum = 0,
              /// <summary>
              /// Enum SubtractEnum for subtract
              /// </summary>
              [EnumMember(Value = "subtract")]
              SubtractEnum = 1,
              /// <summary>
              /// Enum MultiplyEnum for multiply
              /// </summary>
              [EnumMember(Value = "multiply")]
              MultiplyEnum = 2,
              /// <summary>
              /// Enum DivideEnum for divide
              /// </summary>
              [EnumMember(Value = "divide")]
              DivideEnum = 3          }
}
