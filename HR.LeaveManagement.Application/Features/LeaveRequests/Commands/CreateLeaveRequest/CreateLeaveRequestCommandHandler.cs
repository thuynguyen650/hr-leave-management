using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Email;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Exceptions;
using HR.LeaveManagement.Application.Models;
using HR.LeaveManagement.Domain;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveRequests.Commands.CreateLeaveRequest;

public class CreateLeaveRequestCommandHandler : IRequestHandler<CreateLeaveRequestCommand, Unit>
{
    private readonly IEmailSender _emailSender;
    private readonly IMapper _mapper;
    private readonly ILeaveTypeRepository _leaveTypeRepository;
    private readonly ILeaveRequestRepository _leaveRequestRepository;

    public CreateLeaveRequestCommandHandler(IEmailSender emailSender,
        IMapper mapper,
        ILeaveTypeRepository leaveTypeRepository,
        ILeaveRequestRepository leaveRequestRepository)
    {
        _emailSender = emailSender;
        _mapper = mapper;
        _leaveTypeRepository = leaveTypeRepository;
        _leaveRequestRepository = leaveRequestRepository;
    }

    public async Task<Unit> Handle(CreateLeaveRequestCommand request, CancellationToken cancellationToken)
    {
        var validator = new CreateLeaveRequestCommandValidator(_leaveTypeRepository);
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (validationResult.Errors.Any())
            throw new BadRequestException("Invalid leave request", validationResult);

        var leaveRequest = _mapper.Map<LeaveRequest>(request);

        await _leaveRequestRepository.CreateAsync(leaveRequest);

        var email = new EmailMessage
        {
            To = String.Empty,
            Body = $"Your leave request for {request.StartDate:D} to {request.EndDate:D} has been submitted",
            Subject = "Leave Request Submitted"
        };

        await _emailSender.SendEmail(email);

        return Unit.Value;
    }
}
