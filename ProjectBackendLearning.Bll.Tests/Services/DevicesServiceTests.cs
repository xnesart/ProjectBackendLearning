using AutoMapper;
using Moq;
using ProjectBackendLearning.Bll.Services;
using ProjectBackendLearning.Core.DTOs;
using ProjectBackendLearning.Core.Models.Requests;
using ProjectBackendLearning.DataLayer.Repositories;

namespace ProjectBackendLearning.Bll.Tests.Services;

public class DevicesServiceTests
{
    private IDevicesService _sut; //system under test
    private readonly Mock<IDevicesRepository> _deviceRepositoryMock;
    private readonly Mock<IUsersRepository> _usersRepositoryMock;
    private readonly Mock<IUsersService> _usersServiceMock;
    private readonly IMapper _mapper;
    
    public DevicesServiceTests()
    {
        var mapperConfiguration = new MapperConfiguration(cfg => { cfg.AddProfile<RequestMapperProfile>(); });
        _mapper = mapperConfiguration.CreateMapper();
        _deviceRepositoryMock = new Mock<IDevicesRepository>();
        _usersRepositoryMock = new Mock<IUsersRepository>();
        _usersServiceMock = new Mock<IUsersService>();
    }
    
    [Fact]
    public void GetDeviceById_CorrectGuidReceived_DeviceDtoReceived()
    {
        //arrange
        var guid = Guid.NewGuid();
        var device = new DeviceDto()
        {
            Name = "name"
        };
        _sut = new DevicesService(_mapper,_deviceRepositoryMock.Object, _usersServiceMock.Object, _usersRepositoryMock.Object );
        _deviceRepositoryMock.Setup(repo => repo.GetDeviceById(guid)).Returns(device);
        
        //act
        var actual = _sut.GetDeviceById(guid);
       
        //assert
        Assert.Equal(actual, device);
        _deviceRepositoryMock.Verify(x => x.GetDeviceById(It.IsAny<Guid>()), Times.Once());
    }

    [Fact]
    public void AddDeviceToUser_ValidGuidAndRequestReceived_NoErrorsReceived()
    {
        //arrange
        var guid = Guid.NewGuid();
        var request = new AddDeviceToUserRequest() { Name = "name" };
        _usersServiceMock.Setup(repo => repo.GetUserById(guid)).Returns(new UserDto() { Age = 20 });
        
        _sut = new DevicesService(_mapper,_deviceRepositoryMock.Object, _usersServiceMock.Object, _usersRepositoryMock.Object );

        //act
       _sut.AddDeviceToUser(guid, request);
        //assert
        _deviceRepositoryMock.Verify(x => x.AddDeviceToUser(It.IsAny<DeviceDto>()), Times.Once());

    }
    
}