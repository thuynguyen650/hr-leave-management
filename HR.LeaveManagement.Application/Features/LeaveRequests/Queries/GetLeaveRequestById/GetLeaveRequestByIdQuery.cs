using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveRequests.Queries.GetLeaveRequestById;

public record GetLeaveRequestByIdQuery(int Id) : IRequest<LeaveRequestDetails>
{
}
