using FluentValidation;
using HR.LeaveManagement.Application.Contracts.Persistence;

namespace HR.LeaveManagement.Application.Features.LeaveType.Commands.UpdateLeaveType;

public class UpdateLeaveTypeCommandValidator : AbstractValidator<UpdateLeaveTypeCommand>
{
    private readonly ILeaveTypeRepository _repository;

    public UpdateLeaveTypeCommandValidator(ILeaveTypeRepository repository)
    {
        RuleFor(p => p.Id)
            .NotNull()
            .MustAsync(LeaveTypeMustExist)
            .WithMessage("This leave type hasn't been created yet.");

        RuleFor(p => p.Name)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .NotNull()
            .MaximumLength(70).WithMessage("{PropertyName} must not be fewer than 70 characters");

        RuleFor(p => p.DefaultDays)
            .LessThan(100).WithMessage("{PropertyName} cannot exceed 100")
            .GreaterThan(1).WithMessage("{PropertyName} cannot be less than 1");

        RuleFor(p => p)
            .MustAsync(LeaveTypeMustUnique)
            .WithMessage("Leave type is already exists");

        _repository = repository;
    }

    private async Task<bool> LeaveTypeMustExist(int id, CancellationToken token)
    {
        return await _repository.GetByIdAsync(id) != null;
    }

    private async Task<bool> LeaveTypeMustUnique(UpdateLeaveTypeCommand command, CancellationToken token)
    {
        return await _repository.IsLeaveTypeUnique(command.Name);
    }
}
