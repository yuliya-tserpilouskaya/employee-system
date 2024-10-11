using AutoMapper;
using EmployeeManagementSystem.Core.DomainModels;
using EmployeeManagementSystem.Core.Dto;
using EmployeeManagementSystem.Core.Entities;
using EmployeeManagementSystem.Core.Interfaces;

namespace EmployeeManagementSystem.Core.BusinessLogic;

public class EmployeeService : IEmployeeService
{
    private readonly IAsyncRepository<Employee> _employeeRepository;
    private readonly IMapper _mapper;

    public EmployeeService(IAsyncRepository<Employee> employeeRepository, IMapper mapper)
    {
        _employeeRepository = employeeRepository;
        _mapper = mapper;
    }

    public async Task<IReadOnlyCollection<EmployeeDto>> GetEmployeesAsync()
    {
        return _mapper.Map<IReadOnlyCollection<EmployeeDto>>(await _employeeRepository.ListAllAsync());
    }

    public async Task<Result> CreateEmployeeAsync(EmployeeDto employee)
    {
        if (employee == null)
        {
            throw new ArgumentNullException(nameof(employee));
        }

        await _employeeRepository.AddAsync(Employee.Create(employee.FirstName, employee.LastName, employee.Age, employee.Sex).Data);

        return new Result();
    }

    public async Task<Result> UpdateEmployeeAsync(EmployeeDto employee)
    {
        if (employee == null)
        {
            throw new ArgumentNullException(nameof(employee));
        }

        var existingEmployee = await _employeeRepository.GetByIdAsync(employee.Id);

        if (existingEmployee == null)
        {
            throw new ArgumentOutOfRangeException(nameof(employee.Id));
        }

        await _employeeRepository.UpdateAsync(Employee.Update(existingEmployee, employee.FirstName, employee.LastName,
            employee.Age, employee.Sex).Data);

        return new Result();
    }

    public async Task<Result> DeleteEmployeesAsync(IEnumerable<Guid> ids)
    {
        var employeesToDelete = await _employeeRepository.ListByCriteriaAsync(e => ids.Contains(e.Id));

        await _employeeRepository.DeleteAsync(employeesToDelete);

        return new Result();
    }
}