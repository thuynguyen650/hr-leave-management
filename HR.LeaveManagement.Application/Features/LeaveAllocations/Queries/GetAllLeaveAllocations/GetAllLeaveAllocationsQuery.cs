using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveAllocations.Queries.GetAllLeaveAllocations;

public record GetAllLeaveAllocationsQuery : IRequest<List<LeaveAllocationDto>>
{
}
