using HR.LeaveManagement.Application.Features.LeaveType.Queries.GetAllLeaveTypes;

namespace HR.LeaveManagement.Application.Features.LeaveRequests.Queries.GetLeaveRequestById;

public record LeaveRequestDetails
{
    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public required string RequestingEmployeeId { get; set; }

    public required LeaveTypeDto LeaveType { get; set; }

    public int LeaveTypeId { get; set; }

    public DateTime DateRequested { get; set; }

    public string? RequestComments { get; set; }

    public DateTime? DateActioned { get; set; }

    public bool? Approved { get; set; }

    public bool? Canceled { get; set; }
}
