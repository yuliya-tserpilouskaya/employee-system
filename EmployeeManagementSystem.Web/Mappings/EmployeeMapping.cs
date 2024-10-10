using AutoMapper;
using EmployeeManagementSystem.Core.Dto;
using EmployeeManagementSystem.Web.Models;

namespace EmployeeManagementSystem.Web.Mappings;

public class EmployeeMapping : Profile
{
    public EmployeeMapping()
    {
        CreateMap<EmployeeModel, EmployeeDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
            .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
            .ForMember(dest => dest.Age, opt => opt.MapFrom(src => src.Age))
            .ForMember(dest => dest.Sex, opt => opt.MapFrom(src => src.Sex))
            .ReverseMap();
    }
}