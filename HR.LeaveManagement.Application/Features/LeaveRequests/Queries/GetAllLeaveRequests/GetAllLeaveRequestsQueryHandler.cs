using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Persistence;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveRequests.Queries.GetAllLeaveRequests;

public class GetAllLeaveRequestsQueryHandler : IRequestHandler<GetAllLeaveRequestsQuery, List<LeaveRequestDto>>
{
    private readonly ILeaveRequestRepository _leaveRequestRepository;
    private readonly IMapper _mapper;

    public GetAllLeaveRequestsQueryHandler(ILeaveRequestRepository leaveRequestRepository, IMapper mapper)
    {
        _leaveRequestRepository = leaveRequestRepository;
        _mapper = mapper;
    }
    public async Task<List<LeaveRequestDto>> Handle(GetAllLeaveRequestsQuery request, CancellationToken cancellationToken)
    {
        var leaveRequests = await _leaveRequestRepository.GetAllLeaveRequests();

        var data = _mapper.Map<List<LeaveRequestDto>>(leaveRequests);

        return data;
    }
}
