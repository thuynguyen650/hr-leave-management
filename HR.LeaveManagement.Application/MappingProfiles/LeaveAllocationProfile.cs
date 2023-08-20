using AutoMapper;
using HR.LeaveManagement.Application.Features.LeaveAllocations.Commands.CreateLeaveAllocation;
using HR.LeaveManagement.Application.Features.LeaveAllocations.Commands.UpdateLeaveAllocation;
using HR.LeaveManagement.Application.Features.LeaveAllocations.Queries.GetAllLeaveAllocations;
using HR.LeaveManagement.Application.Features.LeaveAllocations.Queries.GetLeaveAllocationDetails;
using HR.LeaveManagement.Domain;

namespace HR.LeaveManagement.Application.MappingProfiles;

public class LeaveAllocationProfile : Profile
{
    public LeaveAllocationProfile()
    {
        CreateMap<LeaveAllocation, LeaveAllocationDto>();

        CreateMap<LeaveAllocation, LeaveAllocationDetailsDto>();

        CreateMap<CreateLeaveAllocationCommand, LeaveAllocation>();

        CreateMap<UpdateLeaveAllocationCommand, LeaveAllocation>();
    }
}
