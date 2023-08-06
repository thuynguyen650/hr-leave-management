using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Logging;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Exceptions;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveType.Queries.GetLeaveTypeDetails;

public class GetLeaveTypeDetailsQueryHandler : IRequestHandler<GetLeaveTypeDetailsQuery, LeaveTypeDetailsDto>
{
    private readonly IMapper _mapper;

    private readonly ILeaveTypeRepository _repository;

    private readonly IAppLogger<GetLeaveTypeDetailsQueryHandler> _logger;

    public GetLeaveTypeDetailsQueryHandler(IMapper mapper, ILeaveTypeRepository repository,
        IAppLogger<GetLeaveTypeDetailsQueryHandler> logger)
    {
        _mapper = mapper;
        _repository = repository;
        _logger = logger;
    }

    public async Task<LeaveTypeDetailsDto> Handle(GetLeaveTypeDetailsQuery request, CancellationToken cancellationToken)
    {
        // query from db
        var leaveType = await _repository.GetByIdAsync(request.Id);

        // verify that record exists
        if (leaveType == null)
        {
            throw new NotFoundException(nameof(leaveType), request.Id);
        }    

        var leaveTypeDetails = _mapper.Map<LeaveTypeDetailsDto>(leaveType);

        _logger.LogInformation("Leave types were retrieved successfully.");

        return leaveTypeDetails;
    }
}
