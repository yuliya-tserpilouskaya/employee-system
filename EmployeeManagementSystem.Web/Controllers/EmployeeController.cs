using AutoMapper;
using EmployeeManagementSystem.Core.Dto;
using EmployeeManagementSystem.Core.Entities;
using EmployeeManagementSystem.Core.Interfaces;
using EmployeeManagementSystem.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagementSystem.Web.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EmployeeController : ControllerBase
{
    private readonly IEmployeeService _employeeService;
    private readonly IMapper _mapper;

    public EmployeeController(IEmployeeService employeeService, IMapper mapper)
    {
        _employeeService = employeeService;
        _mapper = mapper;  
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<EmployeeModel>>> GetEmployees()
    {
        return Ok(await _employeeService.GetEmployeesAsync());
    }

    [HttpPost]
    public async Task<ActionResult<Employee>> CreateEmployee([FromBody] EmployeeModel employee)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        return Ok(await _employeeService.CreateEmployeeAsync(_mapper.Map<EmployeeDto>(employee)));
    }

    [HttpPut]

    public async Task<IActionResult> UpdateEmployee([FromBody] EmployeeModel employee)
    {
        await _employeeService.UpdateEmployeeAsync(_mapper.Map<EmployeeDto>(employee));
        return NoContent();
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteEmployee([FromBody] IEnumerable<Guid> ids)
    {
        var employee = await _employeeService.DeleteEmployeesAsync(ids);
        return NoContent();
    }
}