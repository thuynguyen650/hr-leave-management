using HR.LeaveManagement.Application.Features.LeaveRequests.Shared;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveRequests.Commands.CreateLeaveRequest;

public class CreateLeaveRequestCommand : BaseLeaveRequest, IRequest<Unit>
{
    public string RequestComments { get; set; } = string.Empty;
}
