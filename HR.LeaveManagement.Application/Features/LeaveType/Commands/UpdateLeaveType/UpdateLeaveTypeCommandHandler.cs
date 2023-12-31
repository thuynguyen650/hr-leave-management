﻿using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Logging;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Exceptions;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveType.Commands.UpdateLeaveType;

public class UpdateLeaveTypeCommandHandler : IRequestHandler<UpdateLeaveTypeCommand, Unit>
{
    private readonly IMapper _mapper;

    private readonly ILeaveTypeRepository _leaveTypeRepository;

    private readonly IAppLogger<UpdateLeaveTypeCommandHandler> _logger;

    public UpdateLeaveTypeCommandHandler(IMapper mapper, ILeaveTypeRepository leaveTypeRepository,
        IAppLogger<UpdateLeaveTypeCommandHandler> logger)
    {
        _mapper = mapper;
        _leaveTypeRepository = leaveTypeRepository;
        _logger = logger;
    }

    public async Task<Unit> Handle(UpdateLeaveTypeCommand request, CancellationToken cancellationToken)
    {
        // validate incoming data
        var validator = new UpdateLeaveTypeCommandValidator(_leaveTypeRepository);
        var validationResults = await validator.ValidateAsync(request, cancellationToken);
        if (validationResults.Errors.Any())
        {
            _logger.LogWarning("Validation errors in create request for {0} - {1}", nameof(LeaveType), request.Name);
            throw new BadRequestException("Invalid leave type", validationResults);
        }

        // convert to domain entity object
        var leaveTypeToUpdate = _mapper.Map<Domain.LeaveType>(request);

        // add to database
        await _leaveTypeRepository.UpdateAsync(leaveTypeToUpdate);

        // return Unit value
        return Unit.Value;
    }
}
