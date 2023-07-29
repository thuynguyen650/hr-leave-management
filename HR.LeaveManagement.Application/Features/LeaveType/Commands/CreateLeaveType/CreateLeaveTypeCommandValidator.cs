using FluentValidation;

namespace HR.LeaveManagement.Application.Features.LeaveType.Commands.CreateLeaveType;

public class CreateLeaveTypeCommandValidator : AbstractValidator<CreateLeaveTypeCommand>
{
    public CreateLeaveTypeCommandValidator()
    {
        RuleFor(p => p.Name)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .NotNull()
            .MaximumLength(70).WithMessage("{PropertyName} must not be fewer than 70 characters");

        RuleFor(p => p.DefaultDays)
            .LessThanOrEqualTo(100).WithMessage("{PropertyName} must not be greater than 100")
            .GreaterThan(1).WithMessage("{PropertyName} must be greater than 1");
    }
}
