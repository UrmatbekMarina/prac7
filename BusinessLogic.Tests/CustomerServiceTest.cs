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
    public class CustomerServiceTest
    {

        //private readonly Customer service;
        //private readonly Mock<ICustomerRepository> customerRepositoryMoq;
        //public CustomerServiceTest()
        //{
        //    var repositoryWrapperMoq = new Mock<IRepositoryWrapper>();
        //    customerRepositoryMoq = new Mock<ICustomerRepository>();
        //    repositoryWrapperMoq.Setup(x => x.Customer).Returns(customerRepositoryMoq.Object);

        //    //service = new CustomerService(repositoryWrapperMoq.Object);

        //}
        //[Fact]
        //public async Task CreateAsync_NullCustomer_ShouldThrowNullArgumentExpection()
        //{
        //    var ex = await Assert.ThrowsAnyAsync<ArgumentNullException>(() => service.Create(null));
        //    Assert.IsType<ArgumentNullException>(ex);
        //    customerRepositoryMoq.Verify(x => x.Create(It.IsAny<Customer>()), Times.Never);
        //}
        //[Theory]
        //[MemberData(nameof(GetIncorrectUsers))]
        //public async Task CreateAsyncNewCustomerShouldNotCreateNewCustomer(Customer customer)
        //{
        //    var newCustomer = customer;

        //    var ex = await Assert.ThrowsAnyAsync<ArgumentException>(() => service.Create(newCustomer));
        //    customerRepositoryMoq.Verify(x => x.Create(It.IsAny<Customer>()), Times.Never);
        //    Assert.IsType<ArgumentException>(ex);

        //}
        //public static IEnumerable<object[]> GetIncorrectUsers()
        //{
        //    return new List<object[]>
        //    {
        //        new object[] { new Customer() { Id = int.MaxValue, Name = "" } },
                 
        //    };
        //}
    }
}
