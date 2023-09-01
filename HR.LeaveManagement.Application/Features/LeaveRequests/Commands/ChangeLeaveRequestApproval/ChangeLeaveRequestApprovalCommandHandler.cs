using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Email;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Exceptions;
using HR.LeaveManagement.Application.Models;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveRequests.Commands.ChangeLeaveRequestApproval;

public class ChangeLeaveRequestApprovalCommandHandler : IRequestHandler<ChangeLeaveRequestApprovalCommand, Unit>
{
    private readonly IEmailSender _emailSender;
    private readonly ILeaveRequestRepository _leaveRequestRepository;
    private readonly ILeaveTypeRepository _leaveTypeRepository;
    private readonly IMapper _mapper;

    public ChangeLeaveRequestApprovalCommandHandler(IEmailSender emailSender,
        ILeaveRequestRepository leaveRequestRepository,
        ILeaveTypeRepository leaveTypeRepository,
        IMapper mapper)
    {
        _emailSender = emailSender;
        _leaveRequestRepository = leaveRequestRepository;
        _leaveTypeRepository = leaveTypeRepository;
        _mapper = mapper;
    }

    public async Task<Unit> Handle(ChangeLeaveRequestApprovalCommand request, CancellationToken cancellationToken)
    {
        var leaveRequest = await _leaveRequestRepository.GetByIdAsync(request.Id);

        if (leaveRequest == null) 
            throw new NotFoundException(nameof(leaveRequest), request.Id);

        leaveRequest.Approved = request.Approved;

        await _leaveRequestRepository.UpdateAsync(leaveRequest);

        var email = new EmailMessage
        {
            To = string.Empty,
            Body = $"The approval status of leave request for {leaveRequest.StartDate:D} to {leaveRequest.EndDate:D} has been updated",
            Subject = "Leave request approval status updated"
        };

        _emailSender.SendEmail(email);

        return Unit.Value;
    }
}
