﻿using FluentValidation;
using HR.LeaveManagement.Application.Contracts.Persistence;

namespace HR.LeaveManagement.Application.Features.LeaveType.Commands.CreateLeaveType;

public class CreateLeaveTypeCommandValidator : AbstractValidator<CreateLeaveTypeCommand>
{
    private readonly ILeaveTypeRepository _leaveTypeRepository;

    public CreateLeaveTypeCommandValidator(ILeaveTypeRepository leaveTypeRepository)
    {
        RuleFor(p => p.Name)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .NotNull()
            .MaximumLength(70).WithMessage("{PropertyName} must not be fewer than 70 characters");

        RuleFor(p => p.DefaultDays)
            .LessThanOrEqualTo(100).WithMessage("{PropertyName} must not be greater than 100")
            .GreaterThan(1).WithMessage("{PropertyName} must be greater than 1");

        RuleFor(q => q)
            .MustAsync(LeaveTypeMustUnique)
            .WithMessage("Leave type already exists.");

        _leaveTypeRepository = leaveTypeRepository;
    }

    public Task<bool> LeaveTypeMustUnique(CreateLeaveTypeCommand command, CancellationToken cancellationToken)
    {
        return _leaveTypeRepository.IsLeaveTypeUnique(command.Name);
    }
}
