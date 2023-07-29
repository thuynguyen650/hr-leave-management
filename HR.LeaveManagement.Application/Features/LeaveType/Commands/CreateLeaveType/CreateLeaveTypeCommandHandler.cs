using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Exceptions;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveType.Commands.CreateLeaveType;

public class CreateLeaveTypeCommandHandler : IRequestHandler<CreateLeaveTypeCommand, int>
{
    private readonly IMapper _mapper;

    private readonly ILeaveTypeRepository _repository;

    public CreateLeaveTypeCommandHandler(IMapper mapper, ILeaveTypeRepository repository)
    {
        _mapper = mapper;
        _repository = repository;
    }

    public async Task<int> Handle(CreateLeaveTypeCommand request, CancellationToken cancellationToken)
    {
        // validate incoming data
        var validator = new CreateLeaveTypeCommandValidator(_repository);
        var validationResult = await validator.ValidateAsync(request);
        if (!validationResult.IsValid)
        {
            throw new BadRequestException("Invalid leave type", validationResult);
        }

        // convert command to domain entity object
        var leaveTypeToCreate = _mapper.Map<Domain.LeaveType>(request);

        // add to database
        await _repository.CreateAsync(leaveTypeToCreate);

        // return record id
        return leaveTypeToCreate.Id;
    }
}
