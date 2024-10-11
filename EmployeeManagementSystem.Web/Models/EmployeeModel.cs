using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using EmployeeManagementSystem.Core.Constants;
using EmployeeManagementSystem.Core.Enumerations;

namespace EmployeeManagementSystem.Web.Models;

public class EmployeeModel
{
    public Guid Id { get; set; }

    [Required]
    public string FirstName { get; set; }

    [Required]
    public string LastName { get; set; }

    [Range(AgeConstants.MinAge, AgeConstants.MaxAge)]
    public int Age { get; set; }

    [Required]
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public Sex Sex { get; set; }
}