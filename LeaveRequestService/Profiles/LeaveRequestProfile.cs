using AutoMapper;
using LeaveRequestService.Dtos;
using LeaveRequestService.Models;

namespace LeaveRequestService.Profiles
{
    public class LeaveRequestProfile : Profile
    {
        public LeaveRequestProfile()
        {
            // Cource -> Target
            
            CreateMap<LeaveRequest, LeaveRequestReadDto>().ReverseMap();
            CreateMap<LeaveRequest, LeaveRequestCreateDto>().ReverseMap();

            CreateMap<Employee, EmployeeReadDto>().ReverseMap();

            // Map ExternalEmployeeID (Leave service) from EmployeeID (Employee service)
            // Just for Testing
            CreateMap<EmployeePublishDto, Employee>().ReverseMap()
                .ForMember(des => des.ExternalEmployeeID, opt => opt.MapFrom(src => src.EmployeeID));
        }
    }
}