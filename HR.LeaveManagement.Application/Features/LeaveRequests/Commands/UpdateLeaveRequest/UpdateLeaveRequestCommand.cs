using HR.LeaveManagement.Application.Features.LeaveRequests.Shared;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveRequests.Commands.UpdateLeaveRequest;

public class UpdateLeaveRequestCommand : BaseLeaveRequest, IRequest<Unit>
{
    public int Id { get; set; }

    public string RequestComments { get; set; }

    public bool Canceled { get; set; }
}
