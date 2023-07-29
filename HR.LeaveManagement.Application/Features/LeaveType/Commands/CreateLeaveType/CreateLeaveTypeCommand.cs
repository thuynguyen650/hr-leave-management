using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveType.Commands.CreateLeaveType;

public class CreateLeaveTypeCommand : IRequest<int>
{
    public required string Name { get; set; }

    public int DefaultDays { get; set; }
}
