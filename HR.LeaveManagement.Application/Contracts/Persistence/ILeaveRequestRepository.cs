using HR.LeaveManagement.Domain;

namespace HR.LeaveManagement.Application.Contracts.Persistence;

public interface ILeaveRequestRepository : IGenericRepository<LeaveRequest>
{
    Task<LeaveRequest> GetLeaveRequestById(int id);
    Task<List<LeaveRequest>> GetAllLeaveRequests();
    Task<List<LeaveRequest>> GetLeaveRequestsByEmployeeId(string userId);
}