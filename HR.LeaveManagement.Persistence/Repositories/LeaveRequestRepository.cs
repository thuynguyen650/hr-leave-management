using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Domain;
using HR.LeaveManagement.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace HR.LeaveManagement.Persistence.Repositories;

public class LeaveRequestRepository : GenericRepository<LeaveRequest>, ILeaveRequestRepository
{
    public LeaveRequestRepository(HrDatabaseContext context) : base(context)
    {
    }

    public async Task<List<LeaveRequest>> GetAllLeaveRequests()
    {
        var leaveRequests = await _context.LeaveRequests
            .Include(r => r.LeaveType)
            .ToListAsync();

        return leaveRequests;
    }

    public async Task<LeaveRequest> GetLeaveRequestById(int id)
    {
        var leaveRequest = await _context.LeaveRequests
            .Where(r => r.Id == id)
            .Include(r => r.LeaveType)
            .FirstOrDefaultAsync();

        return leaveRequest;
    }

    public async Task<List<LeaveRequest>> GetLeaveRequestsByEmployeeId(string userId)
    {
        var leaveRequests = await _context.LeaveRequests
            .Where(r => r.RequestingEmployeeId == userId)
            .Include(r => r.LeaveType)
            .ToListAsync();

        return leaveRequests;
    }
}
