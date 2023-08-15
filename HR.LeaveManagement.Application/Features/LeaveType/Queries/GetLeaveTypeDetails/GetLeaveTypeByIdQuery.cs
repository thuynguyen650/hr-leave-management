using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveType.Queries.GetLeaveTypeDetails;

public record GetLeaveTypeByIdQuery(int Id) : IRequest<LeaveTypeDetailsDto>
{
}
