using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveRequests.Queries.GetAllLeaveRequests;

public record GetAllLeaveRequestsQuery : IRequest<List<LeaveRequestDto>>
{
}
