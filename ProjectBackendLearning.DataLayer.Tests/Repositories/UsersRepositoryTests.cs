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
        // var user = new UserDto { UserName = "testUser" };
        // var userId = Guid.NewGuid();
        //
        // var _ctxMock = new Mock<BackMinerContext>();
        //
        // _ctxMock.Setup<DbSet<UserDto>>(x => x.Users.Add(It.IsAny<UserDto>()));
        // _ctxMock.Setup<DbSet<UserDto>>(x => x.Users.Add(It.IsAny<UserDto>());
        // _ctxMock.Setup(x => x.SaveChanges()).Verifiable();
        //
        // var employeeContextMock = new Mock<BackMinerContext>();
        // employeeContextMock.Setup<DbSet<UserDto>>(x => x.Users)
        //     .ReturnsDbSet(TestDataHelper.GetFakeEmployeeList());


        // var _sut = new UsersRepository(_ctxMock.Object);
        //
        //
        //
        // //act
        // var actual = _sut.CreateUser(user);
        //
        // //assert
        // Assert.Equal(actual, userId);
    }
}