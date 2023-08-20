using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Domain;
using HR.LeaveManagement.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace HR.LeaveManagement.Persistence.Repositories;

public class LeaveAllocationRepository : GenericRepository<LeaveAllocation>, ILeaveAllocationRepository
{
    public LeaveAllocationRepository(HrDatabaseContext context) : base(context)
    {
        
    }

    public async Task<bool> AllocationExist(string userId, int leaveTypeId, int period)
    {
        return await _context.LeaveAllocations
            .AnyAsync(a => a.EmployeeId == userId && a.LeaveTypeId == leaveTypeId && a.Period == period);
    }

    public async Task<List<LeaveAllocation>> GetAllLeaveAllocations()
    {
        return await _context.LeaveAllocations
            .Include(a => a.LeaveType)
            .ToListAsync();
    }

    public async Task<LeaveAllocation> GetEmployeeAllocation(string employeeId, int leaveTypeId)
    {
        return await _context.LeaveAllocations
            .Where(a => a.EmployeeId == employeeId && a.LeaveTypeId == leaveTypeId)
            .Include(a => a.LeaveType)
            .FirstOrDefaultAsync();
    }

    public async Task<LeaveAllocation> GetLeaveAllocationById(int id)
    {
        return await _context.LeaveAllocations
            .Where(a => a.Id == id)
            .Include(a => a.LeaveType)
            .FirstOrDefaultAsync();
    }

    public async Task<List<LeaveAllocation>> GetLeaveAllocationsByEmployeeId(string employeeId)
    {
        return await _context.LeaveAllocations
            .Where (a => a.EmployeeId == employeeId)
            .ToListAsync();
    }
}
