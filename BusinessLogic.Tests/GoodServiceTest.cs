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
    public class GoodServiceTest
    {
        private readonly GoodService service;
        private readonly Mock<IGoodRepository> goodRepositoryMoq;
        public GoodServiceTest()
        {
            var repositoryWrapperMoq = new Mock<IRepositoryWrapper>();
            goodRepositoryMoq = new Mock<IGoodRepository>();
            repositoryWrapperMoq.Setup(x => x.Good).Returns(goodRepositoryMoq.Object);

            service = new GoodService(repositoryWrapperMoq.Object);

        }
        [Fact]
        public async Task CreateAsync_NullGood_ShouldThrowNullArgumentExpection()
        {
            var ex = await Assert.ThrowsAnyAsync<ArgumentNullException>(() => service.Create(null));
            Assert.IsType<ArgumentNullException>(ex);
            goodRepositoryMoq.Verify(x => x.Create(It.IsAny<Good>()), Times.Never);
        }
        [Theory]
        [MemberData(nameof(GetIncorrectUsers))]
        public async Task CreateAsyncNewGoodShouldNotCreateNewGood(Good good)
        {
            var newGood = good;

            var ex = await Assert.ThrowsAnyAsync<ArgumentException>(() => service.Create(newGood));
            goodRepositoryMoq.Verify(x => x.Create(It.IsAny<Good>()), Times.Never);
            Assert.IsType<ArgumentException>(ex);

        }
        public static IEnumerable<object[]> GetIncorrectUsers()
        {
            return new List<object[]>
            {
                new object[] { new Good() { CustomerId = int.MaxValue, Discount = int.MaxValue, Id = int.MaxValue, ManufacturerId= null, Name = "", Price = int.MaxValue, Value = int.MaxValue } },

            };
        }
        [Fact]
        public async Task CreateAsyncNewGoodShouldCreateNewGood()
        {
            var newGood = new Good()
            {
                CustomerId = int.MaxValue,
                Discount = int.MaxValue,
                Id = int.MaxValue,
                ManufacturerId = null,
                Name = "s",
                Price = int.MaxValue,  
                Value = int.MaxValue


            };
            await service.Create(newGood);
            goodRepositoryMoq.Verify(x => x.Create(It.IsAny<Good>()), Times.Once);
        }
    }
}
