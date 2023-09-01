using HR.LeaveManagement.Application.Contracts.Email;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Exceptions;
using HR.LeaveManagement.Application.Models;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveRequests.Commands.CancelLeaveRequest;

public class CancelLeaveRequestCommandHandler : IRequestHandler<CancelLeaveRequestCommand, Unit>
{
    private readonly ILeaveRequestRepository _leaveRequestRepository;
    private readonly IEmailSender _emailSender;

    public CancelLeaveRequestCommandHandler(ILeaveRequestRepository leaveRequestRepository,
        IEmailSender emailSender)
    {
        _leaveRequestRepository = leaveRequestRepository;
        _emailSender = emailSender;
    }

    public async Task<Unit> Handle(CancelLeaveRequestCommand request, CancellationToken cancellationToken)
    {
        var leaveRequest = await _leaveRequestRepository.GetByIdAsync(request.Id);

        if (leaveRequest == null)
            throw new NotFoundException(nameof(leaveRequest), request.Id);

        leaveRequest.Canceled = true;

        var email = new EmailMessage
        {
            To = string.Empty,
            Body = $"Your leave request for {leaveRequest.StartDate} to {leaveRequest.EndDate} has been cancelled successfully",
            Subject = "Leave Request Cancelled"
        };

        _emailSender.SendEmail(email);

        return Unit.Value;
    }
}
