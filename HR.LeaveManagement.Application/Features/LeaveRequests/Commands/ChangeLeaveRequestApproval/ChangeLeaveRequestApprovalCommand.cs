using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveRequests.Commands.ChangeLeaveRequestApproval;

public record ChangeLeaveRequestApprovalCommand(int Id, bool Approved) : IRequest<Unit>
{
}
