using BusinessLogic.Services;
using Domain.Interfaces;
using Domain.Models;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Tests
{
    public class UserServiceTest
    {
        private readonly UserService service;
        private readonly Mock<IUserRepository> userRepositoryMoq;
        public UserServiceTest()
        {
            var repositoryWrapperMoq = new Mock<IRepositoryWrapper>();
            userRepositoryMoq = new Mock<IUserRepository>();
            repositoryWrapperMoq.Setup(x => x.User).Returns(userRepositoryMoq.Object); 

            service = new UserService(repositoryWrapperMoq.Object);

        }
        [Fact]
        public async Task CreateAsync_NullUser_ShouldThrowNullArgumentExpection()
        {
            var ex = await Assert.ThrowsAnyAsync<ArgumentNullException>(() => service.Create(null));
            Assert.IsType<ArgumentNullException>(ex);
            userRepositoryMoq.Verify(x => x.Create(It.IsAny<User>()), Times.Never);
        }
        [Theory]
        [MemberData(nameof(GetIncorrectUsers))]
        public async Task CreateAsyncNewUserShouldNotCreateNewUser(User user)
        {
            var newUser = user;
          
            var ex = await Assert.ThrowsAnyAsync<ArgumentException>(() => service.Create(newUser));
            userRepositoryMoq.Verify(x => x.Create(It.IsAny<User>()), Times.Never);
            Assert.IsType<ArgumentException>(ex);

        }
        public static IEnumerable<object[]> GetIncorrectUsers()
        {
            return new List<object[]>
            {
                new object[] { new User() { UserName="", UserAddress="", Id = int.MaxValue, UserPassword = "" , UserRole ="" } },
                 new object[] { new User() { UserName="aa", UserAddress="", Id = int.MaxValue, UserPassword = "" , UserRole ="" } },
                  new object[] { new User() { UserName="aaa", UserAddress="aa", Id = int.MaxValue, UserPassword = "" , UserRole ="" } },

            };
        }
        [Fact]
        public async Task CreateAsyncNewUserShouldCreateNewUser()
        {
            var newUser = new User()
            {
                UserName = "a",
                UserAddress = "s",
                UserPassword = "a",
                UserRole = "a",
                Id = int.MaxValue

            };
            await service.Create(newUser);
            userRepositoryMoq.Verify(x => x.Create(It.IsAny<User>()), Times.Once);
        }


    }

    
}
