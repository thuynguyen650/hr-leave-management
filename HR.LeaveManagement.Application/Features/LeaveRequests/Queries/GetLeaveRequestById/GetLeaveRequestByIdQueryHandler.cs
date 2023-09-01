using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Exceptions;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveRequests.Queries.GetLeaveRequestById;

public class GetLeaveRequestByIdQueryHandler : IRequestHandler<GetLeaveRequestByIdQuery, LeaveRequestDetails>
{
    private readonly ILeaveRequestRepository _leaveRequestRepository;
    private readonly IMapper _mapper;

    public GetLeaveRequestByIdQueryHandler(ILeaveRequestRepository leaveRequestRepository, IMapper mapper)
    {
        _leaveRequestRepository = leaveRequestRepository;
        _mapper = mapper;
    }
    public async Task<LeaveRequestDetails> Handle(GetLeaveRequestByIdQuery request, CancellationToken cancellationToken)
    {
        var leaveRequest = await _leaveRequestRepository.GetLeaveRequestById(request.Id);

        if (leaveRequest == null)
        {
            throw new NotFoundException(nameof(leaveRequest), request.Id);
        }

        var data = _mapper.Map<LeaveRequestDetails>(leaveRequest);

        return data;
    }
}
