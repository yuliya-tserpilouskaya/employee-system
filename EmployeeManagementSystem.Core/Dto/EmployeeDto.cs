using EmployeeManagementSystem.Core.Enumerations;

namespace EmployeeManagementSystem.Core.Dto;

public class EmployeeDto
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public int Age { get; set; }
    public Sex Sex { get; set; }
}