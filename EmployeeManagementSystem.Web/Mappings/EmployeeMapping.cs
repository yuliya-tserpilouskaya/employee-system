using AutoMapper;
using EmployeeManagementSystem.Core.Dto;
using EmployeeManagementSystem.Core.Entities;
using EmployeeManagementSystem.Web.Models;

namespace EmployeeManagementSystem.Web.Mappings;

public class EmployeeMapping : Profile
{
    public EmployeeMapping()
    {
        CreateMap<EmployeeModel, EmployeeDto>().ReverseMap();
        CreateMap<Employee, EmployeeDto>().ReverseMap();
    }
}