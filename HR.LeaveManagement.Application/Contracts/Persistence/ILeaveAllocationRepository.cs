using HR.LeaveManagement.Domain;

namespace HR.LeaveManagement.Application.Contracts.Persistence;

public interface ILeaveAllocationRepository : IGenericRepository<LeaveAllocation>
{
    Task<List<LeaveAllocation>> GetAllLeaveAllocations();

    Task<LeaveAllocation> GetLeaveAllocationById(int id);

    Task<List<LeaveAllocation>> GetLeaveAllocationsByEmployeeId(string employeeId);

    Task<bool> AllocationExist(string userId, int leaveTypeId, int period);

    Task<LeaveAllocation> GetEmployeeAllocation(string employeeId, int leaveTypeId);
}
