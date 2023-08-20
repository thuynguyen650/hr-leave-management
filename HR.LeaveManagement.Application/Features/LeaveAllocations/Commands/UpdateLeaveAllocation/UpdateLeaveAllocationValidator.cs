using FluentValidation;
using HR.LeaveManagement.Application.Contracts.Persistence;

namespace HR.LeaveManagement.Application.Features.LeaveAllocations.Commands.UpdateLeaveAllocation;

public class UpdateLeaveAllocationValidator : AbstractValidator<UpdateLeaveAllocationCommand>
{
    private readonly ILeaveTypeRepository _leaveTypeRepository;
    private readonly ILeaveAllocationRepository _leaveAllocationRepository;

    public UpdateLeaveAllocationValidator(ILeaveTypeRepository leaveTypeRepository,
        ILeaveAllocationRepository leaveAllocationRepository)
    {
        RuleFor(a => a.NumberOfDays)
            .GreaterThan(0)
            .WithMessage("Number of days must be greater than 0.");

        RuleFor(a => a.LeaveTypeId)
            .MustAsync(LeaveTypeMustExist)
            .WithMessage("{PropertyName} does not exist.");

        RuleFor(a => a.Id)
            .MustAsync(LeaveAllocationMustExist)
            .WithMessage("Leave allocation does not exist.");

        _leaveTypeRepository = leaveTypeRepository;
        _leaveAllocationRepository = leaveAllocationRepository;
    }

    private async Task<bool> LeaveTypeMustExist(int leaveTypeId, CancellationToken cancellationToken)
    {
        return await _leaveTypeRepository.GetByIdAsync(leaveTypeId) != null;
    }

    private async Task<bool> LeaveAllocationMustExist(int leaveAllocationId, CancellationToken cancellationToken)
    {
        return await _leaveAllocationRepository.GetByIdAsync(leaveAllocationId) != null;
    }
}
