using Microsoft.AspNetCore.Mvc;
using Moq;
using ProjectBackendLearning.Bll.Services;
using ProjectBackendLearning.Controllers;
using ProjectBackendLearning.Core.DTOs;
using ProjectBackendLearning.Core.Models.Requests;
using ProjectBackendLearning.Core.Models.Responses;

namespace ProjectBackendLearning.Api.Tests.Controllers;

public class UsersControllerTests
{
    private readonly UsersController _sut;
    private readonly Mock<IUsersService> _usersMockService;
    private readonly Mock<IDevicesService> _devicesMockService;

    public UsersControllerTests()
    {
        _usersMockService = new Mock<IUsersService>();
        _devicesMockService = new Mock<IDevicesService>();
        _sut = new UsersController(_usersMockService.Object, _devicesMockService.Object);
    }

    [Fact]
    public void GetUsers_Called_UsersListReturned()
    {
        //arrange
        var expectedUsers = new List<UserDto>
        {
            new UserDto { Id = Guid.NewGuid(), UserName = "User1" },
            new UserDto { Id = Guid.NewGuid(), UserName = "User2" }
        };
        _usersMockService.Setup(service => service.GetUsers()).Returns(expectedUsers);

        //act
        var result = _sut.GetUsers();

        //assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnValue = Assert.IsType<List<UserDto>>(okResult.Value);
        Assert.Equal(expectedUsers.Count, returnValue.Count);
        _usersMockService.Verify(x => x.GetUsers(), Times.Once);
    }

    [Fact]
    public void CreateUser_CorrectRequestSent_GuidReceived()
    {
        //arrange
        var expected = Guid.NewGuid();
        var request = new CreateUserRequest()
        {
            Age = 20, Email = "test@mail.ru", Password = "test", UserName = "Ivan",
        };

        _usersMockService.Setup(service => service.CreateUser(request)).Returns(expected);

        //act
        var result = _sut.CreateUser(request);

        //assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        Assert.IsType<Guid>(okResult.Value);
        _usersMockService.Verify(x => x.CreateUser(request), Times.Once);
    }  
    
    [Fact]
    public void Login_CorrectRequestSent_AuthenticationResponseReceived()
    {
        //arrange
        var expected = new AuthenticationResponse();
        var request = new LoginUserRequest() { Password = "password", UserName = "user" };

        _usersMockService.Setup(service => service.Login(request)).Returns(expected);

        //act
        var result = _sut.Login(request);

        //assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        Assert.IsType<AuthenticationResponse>(okResult.Value);
        _usersMockService.Verify(x => x.Login(request), Times.Once);
    } 
    
    [Fact]
    public void UpdateUser_CorrectRequestSent_GuidReceived()
    {
        //arrange
        var expected = Guid.NewGuid();
        var request = new UpdateUserRequest{Age = 20, Email = "test"};

        _usersMockService.Setup(service => service.UpdateUser(request)).Returns(expected);

        //act
        var result = _sut.UpdateUser(request);

        //assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        Assert.IsType<Guid>(okResult.Value);
        _usersMockService.Verify(x => x.UpdateUser(request), Times.Once);
    }
    
    [Fact]
    public void DeleteUserById_CorrectGuidSent_NoErrorsReceived()
    {
        var userId = Guid.NewGuid();
        _usersMockService.Setup(service => service.DeleteUserById(userId)).Verifiable();

        // Act
        var result = _sut.DeleteUserById(userId);

        // Assert
        Assert.IsType<NotFoundResult>(result);
        _usersMockService.Verify(service => service.DeleteUserById(userId), Times.Once);
    }    
    
    [Fact]
    public void GetDevicesByUserId_CorrectGuidSent_DevicesReceived()
    {
        //arrange
        var userId = Guid.NewGuid();
        var expectedDevices = new List<DeviceDto>
        {
            new DeviceDto(){Address = "address"}
        };
        _devicesMockService.Setup(service => service.GetDevicesByUserId(userId)).Returns(expectedDevices);

        //act
        var result = _sut.GetDevicesByUserId(userId);

        //assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnValue = Assert.IsType<List<DeviceDto>>(okResult.Value);
        Assert.Equal(expectedDevices.Count, returnValue.Count);
        _devicesMockService.Verify(x => x.GetDevicesByUserId(userId), Times.Once);
    }
}