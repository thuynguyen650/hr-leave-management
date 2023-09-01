using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Exceptions;
using HR.LeaveManagement.Domain;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveAllocations.Queries.GetLeaveAllocationDetails;

public class GetLeaveAllocationByIdQueryHandler : IRequestHandler<GetLeaveAllocationByIdQuery, LeaveAllocationDetailsDto>
{
    private readonly ILeaveAllocationRepository _leaveAllocationRepository;
    private readonly IMapper _mapper;

    public GetLeaveAllocationByIdQueryHandler(ILeaveAllocationRepository leaveAllocationRepository, IMapper mapper)
    {
        _leaveAllocationRepository = leaveAllocationRepository;
        _mapper = mapper;
    }
    public async Task<LeaveAllocationDetailsDto> Handle(GetLeaveAllocationByIdQuery request, CancellationToken cancellationToken)
    {
        var leaveAllocation = await _leaveAllocationRepository.GetLeaveAllocationById(request.Id);

        if (leaveAllocation == null)
        {
            throw new NotFoundException(nameof(leaveAllocation), request.Id);
        }

        var data = _mapper.Map<LeaveAllocationDetailsDto>(leaveAllocation);

        return data;
    }
}
