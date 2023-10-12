using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Logging;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Features.LeaveType.Queries.GetAllLeaveTypes;
using HR.LeaveManagement.Application.MappingProfiles;
using HR.LeaveManagement.Application.UnitTests.Mocks;
using Moq;
using Shouldly;

namespace HR.LeaveManagement.Application.UnitTests.Features.LeaveType;

public class GetLeaveTypesQueryHandlerTests
{
    private readonly Mock<ILeaveTypeRepository> _mockRepo;
    private readonly IMapper _mapper;
    private readonly Mock<IAppLogger<GetAllLeaveTypesQueryHandler>> _appLogger;

    public GetLeaveTypesQueryHandlerTests() {
        _mockRepo = MockLeaveTypeRepository.GetLeaveTypes();

        var mapperConfig = new MapperConfiguration(c =>
        {
            c.AddProfile<LeaveTypeProfile>();
        });

        _mapper = mapperConfig.CreateMapper();

        _appLogger = new Mock<IAppLogger<GetAllLeaveTypesQueryHandler>>();
    }

    [Fact]
    public async Task GetLeaveTypeListTest()
    {
        var handler = new GetAllLeaveTypesQueryHandler(_mapper, _mockRepo.Object);

        var result = await handler.Handle(new GetAllLeaveTypesQuery(), CancellationToken.None);

        result.ShouldBeOfType<List<LeaveTypeDto>>();
        result.Count.ShouldBe(3);
    }
}
