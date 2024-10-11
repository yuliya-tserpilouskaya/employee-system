using EmployeeManagementSystem.Core.DomainModels;
using EmployeeManagementSystem.Core.Dto;

namespace EmployeeManagementSystem.Core.Interfaces;

public interface IEmployeeService
{
    Task<IReadOnlyCollection<EmployeeDto>> GetEmployeesAsync();
    Task<Result> CreateEmployeeAsync(EmployeeDto employee);
    Task<Result> UpdateEmployeeAsync(EmployeeDto employee);
    Task<Result> DeleteEmployeesAsync(IEnumerable<Guid> ids);
}