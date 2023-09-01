using HR.LeaveManagement.Application.Features.LeaveRequests.Commands.CancelLeaveRequest;
using HR.LeaveManagement.Application.Features.LeaveRequests.Commands.ChangeLeaveRequestApproval;
using HR.LeaveManagement.Application.Features.LeaveRequests.Commands.CreateLeaveRequest;
using HR.LeaveManagement.Application.Features.LeaveRequests.Commands.DeleteLeaveRequest;
using HR.LeaveManagement.Application.Features.LeaveRequests.Commands.UpdateLeaveRequest;
using HR.LeaveManagement.Application.Features.LeaveRequests.Queries.GetAllLeaveRequests;
using HR.LeaveManagement.Application.Features.LeaveRequests.Queries.GetLeaveRequestById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HR.LeaveManagement.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LeaveRequestsController : ControllerBase
{
    private readonly IMediator _mediator;

    public LeaveRequestsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<List<LeaveRequestDto>> GetAllLeaveRequests()
    {
        return await _mediator.Send(new GetAllLeaveRequestsQuery());
    }

    [HttpGet("id")]
    public async Task<LeaveRequestDetails> GetLeaveRequestById(int id)
    {
        return await _mediator.Send(new GetLeaveRequestByIdQuery(id));
    }

    [HttpPost]
    [ProducesResponseType(201)]
    [ProducesResponseType(400)]
    [ProducesDefaultResponseType]
    public async Task<Unit> CreateLeaveRequest(CreateLeaveRequestCommand request)
    {
        return await _mediator.Send(request);
    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(400)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult> UpdateLeaveRequest(UpdateLeaveRequestCommand request)
    {
        await _mediator.Send(request);
        return NoContent();
    }

    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult> DeleteLeaveRequest(int Id)
    {
        await _mediator.Send(new DeleteLeaveRequestCommand(Id));
        return NoContent();
    }

    [HttpPut]
    [Route("ChangeApproval")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(400)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult> ChangeLeaveRequestApproval(ChangeLeaveRequestApprovalCommand request)
    {
        await _mediator.Send(request);
        return NoContent();
    }

    [HttpPut]
    [Route("CancelRequest")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(400)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult> CancelLeaveRequest(CancelLeaveRequestCommand request)
    {
        await _mediator.Send(request);
        return NoContent();
    }
}
