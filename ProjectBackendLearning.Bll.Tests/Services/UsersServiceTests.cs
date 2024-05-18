using System.Runtime.InteropServices;
using AutoMapper;
using FluentValidation;
using Moq;
using Moq.EntityFrameworkCore;
using ProjectBackendLearning.Bll.Services;
using ProjectBackendLearning.Core.DTOs;
using ProjectBackendLearning.Core.Exceptions;
using ProjectBackendLearning.Core.Models.Requests;
using ProjectBackendLearning.Core.Validation;
using ProjectBackendLearning.DataLayer.Repositories;
using ValidationException = ProjectBackendLearning.Core.Exceptions.ValidationException;

namespace ProjectBackendLearning.Bll.Tests.Services;

public class UsersServiceTests
{
    private IUsersService _sut; //system under test
    private readonly Mock<IUsersRepository> _userRepositoryMock;
    private readonly IMapper _mapper;
    private readonly IValidator<CreateUserRequest> _userValidator;
    private readonly IValidator<UpdateUserRequest> _userUpdateValidator;

    public UsersServiceTests()
    {
        var mapperConfiguration = new MapperConfiguration(cfg => { cfg.AddProfile<RequestMapperProfile>(); });
        _mapper = mapperConfiguration.CreateMapper();
        _userValidator = new UserRequestValidator();
        _userUpdateValidator = new UserUpdateValidator();
        _userRepositoryMock = new Mock<IUsersRepository>();
    }

    [Fact]
    public void CreateUser_ValidRequestSent_GuidReceived()
    {
        //arrange
        var expectedGuid = Guid.NewGuid();
        _userRepositoryMock.Setup(repo => repo.CreateUser(It.IsAny<UserDto>())).Returns(expectedGuid);

        _sut = new UsersService(_userRepositoryMock.Object, _mapper, _userValidator, _userUpdateValidator);
        var request = new CreateUserRequest()
        {
            Age = 20,
            Email = "test@example.com",
            Password = "JgD$d2*ZS3Jp99",
            UserName = "user"
        };

        //act
        var actual = _sut.CreateUser(request);

        //assert
        Assert.Equal(expectedGuid, actual);
        _userRepositoryMock.Verify(x => x.CreateUser(It.IsAny<UserDto>()), Times.Once());
    }
    
    [Theory]
    [InlineData(null)]
    [InlineData("password")]
    [InlineData("2Pasrd*")]
    [InlineData("password*password")]
    public void CreateUser_RequestWithInvalidPasswordSent_PasswordErrorReceived(string password)
    {
        //arrange
        var expectedGuid = Guid.NewGuid();
        _userRepositoryMock.Setup(repo => repo.CreateUser(It.IsAny<UserDto>())).Returns(expectedGuid);

        _sut = new UsersService(_userRepositoryMock.Object, _mapper, _userValidator, _userUpdateValidator);
        var invalidRequest = new CreateUserRequest()
        {
            Age = 20,
            Email = "test@gmail.com",
            Password = password,
            UserName = "user"
        };

        //act, assert
        Assert.Throws<ValidationException>(() => _sut.CreateUser(invalidRequest));
        _userRepositoryMock.Verify(x => x.CreateUser(It.IsAny<UserDto>()), Times.Never());
    }
    
    [Theory]
    [InlineData(null)]
    [InlineData(10)]
    [InlineData(17)]
    [InlineData(200)]
    public void CreateUser_RequestWithInvalidAgeSent_AgeErrorReceived(int age)
    {
        //arrange
        var expectedGuid = Guid.NewGuid();
        _userRepositoryMock.Setup(repo => repo.CreateUser(It.IsAny<UserDto>())).Returns(expectedGuid);

        _sut = new UsersService(_userRepositoryMock.Object, _mapper, _userValidator, _userUpdateValidator);
        var invalidRequest = new CreateUserRequest()
        {
            Age = age,
            Email = "test@gmail.com",
            Password = "*QA*z#!7w5iQt6",
            UserName = "user"
        };

        //act, assert
        Assert.Throws<ValidationException>(() => _sut.CreateUser(invalidRequest));
        _userRepositoryMock.Verify(x => x.CreateUser(It.IsAny<UserDto>()), Times.Never());
    }
    
    [Fact]
    public void GetUsers_Called_UsersReceived()
    {
        //arrange
        var expectedList = new List<UserDto>()
        {
            new UserDto() { Age = 20 }, new UserDto() { Age = 30 },
        };
        _userRepositoryMock.Setup(repo => repo.GetUsers()).Returns(expectedList);

        _sut = new UsersService(_userRepositoryMock.Object, _mapper, _userValidator, _userUpdateValidator);
        

        //act
        var actual = _sut.GetUsers();

        //assert
        Assert.Equal(expectedList, actual);
        _userRepositoryMock.Verify(x => x.GetUsers(), Times.Once());
    } 
    
    [Fact]
    public void GetUserById_ValidGuid_UserDtoReceived()
    {
        //arrange
        Guid guid = new Guid();
        var expectedDto = new UserDto()
        {
            Id = guid,
            Age = 20,
            Email = "test@example.com",
            UserName = "user"
        };
       
        _userRepositoryMock.Setup(repo => repo.GetUserById(It.IsAny<Guid>())).Returns(expectedDto);
        _sut = new UsersService(_userRepositoryMock.Object, _mapper, _userValidator, _userUpdateValidator);

        //act
        var actual = _sut.GetUserById(guid);

        //assert
        Assert.Equal(expectedDto, actual);
        _userRepositoryMock.Verify(x => x.GetUserById(guid), Times.Once());
    }
    
    [Fact]
    public void DeleteUserById_ValidGuid_NoErrorsReceived()
    {
        //arrange
        Guid guid = new Guid();
        var userDto = new UserDto()
        {
            Id = guid,
            Age = 20,
            Email = "test@example.com",
            UserName = "user"
        };
       
        _userRepositoryMock.Setup(repo => repo.GetUserById(It.IsAny<Guid>())).Returns(userDto);
        _sut = new UsersService(_userRepositoryMock.Object, _mapper, _userValidator, _userUpdateValidator);

        //act
        _sut.DeleteUserById(guid);

        //assert
        _userRepositoryMock.Verify(x => x.DeleteUser(userDto), Times.Once());
    }  
    
    [Fact]
    public void DeleteUserById_InvalidGuid_UserNotFoundErrorReceived()
    {
        //arrange
        Guid guid = new Guid();
        var userDto = new UserDto()
        {
            Id = guid,
            Age = 20,
            Email = "test@example.com",
            UserName = "user"
        };
       
        _userRepositoryMock.Setup(repo => repo.GetUserById(It.IsAny<Guid>())).Returns((UserDto)null);
        _sut = new UsersService(_userRepositoryMock.Object, _mapper, _userValidator, _userUpdateValidator);

        //act, assert
        Assert.Throws<NotFoundException>(() => _sut.DeleteUserById(guid));
        _userRepositoryMock.Verify(x => x.GetUserById(guid), Times.Once());
        _userRepositoryMock.Verify(x => x.DeleteUser(userDto), Times.Never());
    }
}