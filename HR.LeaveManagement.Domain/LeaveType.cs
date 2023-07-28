﻿using HR.LeaveManagement.Domain.Common;

namespace HR.LeaveManagement.Domain;

public class LeaveType : BaseEntity
{
    public required string Name { get; set; }

    public int DefaultDays { get; set; }
}
