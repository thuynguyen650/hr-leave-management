﻿namespace HR.LeaveManagement.Application.Features.LeaveType.Queries.GetLeaveTypeDetails;

public record LeaveTypeDetailsDto
{
    public int Id { get; set; }

    public required string Name { get; set; }

    public int DefaultDays { get; set; }

    public DateTime? DateCreated { get; set; }

    public DateTime? DateModified { get; set; }
}
