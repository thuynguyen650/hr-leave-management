namespace HR.LeaveManagement.Application.Features.LeaveType.Queries.GetAllLeaveTypes;

public class LeaveTypeDto
{
    public int Id { get; set; }

    public required string Name { get; set; }

    public int DefaultDays { get; set; }
}
