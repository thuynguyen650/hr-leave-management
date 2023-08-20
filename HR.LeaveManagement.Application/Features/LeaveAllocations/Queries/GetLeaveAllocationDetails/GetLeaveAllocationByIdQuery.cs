using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveAllocations.Queries.GetLeaveAllocationDetails;

public record GetLeaveAllocationByIdQuery(int Id) : IRequest<LeaveAllocationDetailsDto>
{
}
