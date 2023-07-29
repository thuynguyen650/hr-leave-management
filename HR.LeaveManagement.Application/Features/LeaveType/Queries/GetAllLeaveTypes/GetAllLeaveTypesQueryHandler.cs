using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Persistence;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveType.Queries.GetAllLeaveTypes;

public class GetAllLeaveTypesQueryHandler : IRequestHandler<GetAllLeaveTypesQuery, List<LeaveTypeDto>>
{
    private readonly IMapper _mapper;

    private readonly ILeaveTypeRepository _repository;
    public GetAllLeaveTypesQueryHandler(IMapper mapper, ILeaveTypeRepository repository)
    {
        _mapper = mapper;
        _repository = repository;
    }

    public async Task<List<LeaveTypeDto>> Handle(GetAllLeaveTypesQuery request, CancellationToken cancellationToken)
    {
        // Query the database
        var leaveTypes = await _repository.GetAsync();

        // convert data objects to DTO objects
        var data = _mapper.Map<List<LeaveTypeDto>>(leaveTypes);

        // return list of DTO objects
        return data;
    }
}
