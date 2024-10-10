using EmployeeManagementSystem.Core.DomainModels;
using EmployeeManagementSystem.Core.Dto;
using EmployeeManagementSystem.Core.Entities;

namespace EmployeeManagementSystem.Core.Interfaces;

public interface IEmployeeService
{
    Task<IEnumerable<Employee>> GetEmployeesAsync();
    Task<Result> CreateEmployeeAsync(EmployeeDto employee);
    Task<Result> UpdateEmployeeAsync(EmployeeDto employee);
    Task<Result> DeleteEmployeesAsync(IEnumerable<Guid> ids);
}