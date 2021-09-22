using AutoMapper;
using EmployeeService.Dtos;
using EmployeeService.Models;

namespace EmployeeService.Profiles
{
    public class EmployeeProfile : Profile
    {
        public EmployeeProfile()
        {
            // Cource -> Target
            CreateMap<Employee, EmployeeReadDto>().ReverseMap();
            CreateMap<Employee, EmployeeCreateDto>().ReverseMap();
            CreateMap<EmployeeReadDto, EmployeePublishDto>().ReverseMap();
        }
    }
}