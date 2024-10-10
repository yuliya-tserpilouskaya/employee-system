using AutoMapper;
using EmployeeManagementSystem.Core.Dto;
using EmployeeManagementSystem.Core.Interfaces;
using EmployeeManagementSystem.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net;

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
    [ProducesResponseType(typeof(IReadOnlyCollection<EmployeeModel>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> GetEmployees()
    {
        return Ok(await _employeeService.GetEmployeesAsync());
    }

    [HttpPost]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    public async Task<IActionResult> CreateEmployee([FromBody] EmployeeModel employee)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var result = await _employeeService.CreateEmployeeAsync(_mapper.Map<EmployeeDto>(employee));

        return result.Succeed ? Ok() : BadRequest();
    }

    [HttpPut]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    public async Task<IActionResult> UpdateEmployee([FromBody] EmployeeModel employee)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var result = await _employeeService.UpdateEmployeeAsync(_mapper.Map<EmployeeDto>(employee));

        return result.Succeed ? Ok() : BadRequest();
    }

    [HttpDelete]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    public async Task<IActionResult> DeleteEmployee([FromBody] IEnumerable<Guid> ids)
    {
        var result = await _employeeService.DeleteEmployeesAsync(ids);

        return result.Succeed ? Ok() : BadRequest();
    }
}