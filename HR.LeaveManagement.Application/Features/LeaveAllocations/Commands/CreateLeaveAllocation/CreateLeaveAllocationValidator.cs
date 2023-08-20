using FluentValidation;
using HR.LeaveManagement.Application.Contracts.Persistence;

namespace HR.LeaveManagement.Application.Features.LeaveAllocations.Commands.CreateLeaveAllocation;

public class CreateLeaveAllocationValidator : AbstractValidator<CreateLeaveAllocationCommand>
{
    private readonly ILeaveTypeRepository _leaveTypeRepository;

    public CreateLeaveAllocationValidator(ILeaveTypeRepository leaveTypeRepository)
    {
        RuleFor(a => a.LeaveTypeId)
            .GreaterThanOrEqualTo(1)
            .WithMessage("Leave type id is not valid")
            .MustAsync(LeaveTypeMustExist);
        _leaveTypeRepository = leaveTypeRepository;
    }

    public async Task<bool> LeaveTypeMustExist(int leaveTypeId, CancellationToken cancellationToken)
    {
        return await _leaveTypeRepository.GetByIdAsync(leaveTypeId) != null;
    }
}
