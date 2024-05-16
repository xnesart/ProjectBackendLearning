using Microsoft.EntityFrameworkCore;
using Moq;
using Moq.EntityFrameworkCore;
using ProjectBackendLearning.Core.DTOs;
using ProjectBackendLearning.DataLayer.Repositories;

namespace ProjectBackendLearning.DataLayer.Tests.Repositories;

public class UsersRepositoryTests
{
    private IUsersRepository _sut;
    private Mock<BackMinerContext> _ctxMock;

    public UsersRepositoryTests()
    {
    }

    [Fact]
    public void CreateUser_ValidUserDtoSent_GuidReturned()
    {
        //arrange
        var user = new UserDto { UserName = "testUser" };
        var mockUsers = new List<UserDto>() { new UserDto() { Id = Guid.NewGuid() } };

        _ctxMock = new Mock<BackMinerContext>();
        _ctxMock.Setup<DbSet<UserDto>>(x => x.Users)
            .ReturnsDbSet(mockUsers);
        _ctxMock.Setup(x => x.SaveChanges()).Verifiable();
        
        var _sut = new UsersRepository(_ctxMock.Object);
        
        //act
       _sut.CreateUser(user);

        //assert
        Assert.Equal(1, this._ctxMock.Object.Users.Count());
        _ctxMock.Verify(x => x.SaveChanges(), Times.Once);

    }   
    
    [Fact]
    public void DeleteUser_ValidUserDtoSent_NoErrorsReceived()
    {
        // Arrange
        var user = new UserDto { UserName = "testUser" };
        var mockUsers = new List<UserDto>() { user };

        var _ctxMock = new Mock<BackMinerContext>();
        _ctxMock.Setup<DbSet<UserDto>>(x => x.Users)
            .ReturnsDbSet(mockUsers);

        var _sut = new UsersRepository(_ctxMock.Object);

        // Act
        _sut.DeleteUser(user);

        // Assert
        _ctxMock.Verify(x => x.SaveChanges(), Times.Once);
    }
}