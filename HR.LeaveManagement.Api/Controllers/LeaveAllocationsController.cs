using HR.LeaveManagement.Application.Features.LeaveAllocations.Commands.CreateLeaveAllocation;
using HR.LeaveManagement.Application.Features.LeaveAllocations.Commands.DeleteLeaveAllocation;
using HR.LeaveManagement.Application.Features.LeaveAllocations.Commands.UpdateLeaveAllocation;
using HR.LeaveManagement.Application.Features.LeaveAllocations.Queries.GetAllLeaveAllocations;
using HR.LeaveManagement.Application.Features.LeaveAllocations.Queries.GetLeaveAllocationDetails;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HR.LeaveManagement.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LeaveAllocationsController : ControllerBase
{
    private readonly IMediator _mediator;

    public LeaveAllocationsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<List<LeaveAllocationDto>> GetAllLeaveAllocations()
    {
        return await _mediator.Send(new GetAllLeaveAllocationsQuery());
    }

    [HttpGet("id")]
    public async Task<LeaveAllocationDetailsDto> GetLeaveAllocationById(int id)
    {
        return await _mediator.Send(new GetLeaveAllocationByIdQuery(id));
    }

    [HttpPost]
    [ProducesResponseType(201)]
    [ProducesResponseType(400)]
    [ProducesDefaultResponseType]
    public async Task<int> CreateLeaveAllocation(CreateLeaveAllocationCommand request)
    {
        return await _mediator.Send(request);
    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(400)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult> UpdateLeaveAllocation(UpdateLeaveAllocationCommand request)
    {
        await _mediator.Send(request);
        return NoContent();
    }

    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult> DeleteLeaveAllocation(int Id)
    {
        await _mediator.Send(new DeleteLeaveAllocationCommand(Id));
        return NoContent();
    }
}
