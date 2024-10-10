using EmployeeManagementSystem.Core.Constants;
using EmployeeManagementSystem.Core.Enumerations;
using System.ComponentModel.DataAnnotations;

namespace EmployeeManagementSystem.Core.Dto;

public class EmployeeDto
{
    public Guid Id { get; set; }
    [Required]
    public string FirstName { get; set; }

    [Required]
    public string LastName { get; set; }

    [Range(AgeConstants.MinAge, AgeConstants.MaxAge)]
    public int Age { get; set; }

    [Required]
    public Sex Sex { get; set; }
}