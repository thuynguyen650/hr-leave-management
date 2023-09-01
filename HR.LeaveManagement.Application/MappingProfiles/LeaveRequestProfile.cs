using AutoMapper;
using HR.LeaveManagement.Application.Features.LeaveRequests.Queries.GetAllLeaveRequests;
using HR.LeaveManagement.Domain;

namespace HR.LeaveManagement.Application.MappingProfiles;

public class LeaveRequestProfile : Profile
{
    public LeaveRequestProfile()
    {
        CreateMap<LeaveRequest, LeaveRequestDto>();
    }
}
