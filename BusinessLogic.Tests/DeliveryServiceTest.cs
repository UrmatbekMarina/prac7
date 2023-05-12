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
    public class DeliveryServiceTest
    {
        private readonly DeliveryService service;
        private readonly Mock<IDeliveryRepository> deliveryRepositoryMoq;
        public DeliveryServiceTest()
        {
            var repositoryWrapperMoq = new Mock<IRepositoryWrapper>();
            deliveryRepositoryMoq = new Mock<IDeliveryRepository>();
            repositoryWrapperMoq.Setup(x => x.Delivery).Returns(deliveryRepositoryMoq.Object);

            service = new DeliveryService(repositoryWrapperMoq.Object);

        }
        [Fact]
        public async Task CreateAsync_NullDelivery_ShouldThrowNullArgumentExpection()
        {
            var ex = await Assert.ThrowsAnyAsync<ArgumentNullException>(() => service.Create(null));
            Assert.IsType<ArgumentNullException>(ex);
            deliveryRepositoryMoq.Verify(x => x.Create(It.IsAny<Delivery>()), Times.Never);
        }
        [Theory]
        [MemberData(nameof(GetIncorrectUsers))]
        public async Task CreateAsyncNewDeliveryShouldNotCreateNewDelivery(Delivery delivery)
        {
            var newDelivery = delivery;

            var ex = await Assert.ThrowsAnyAsync<ArgumentException>(() => service.Create(newDelivery));
           deliveryRepositoryMoq.Verify(x => x.Create(It.IsAny<Delivery>()), Times.Never);
            Assert.IsType<ArgumentException>(ex);

        }
        public static IEnumerable<object[]> GetIncorrectUsers()
        {
            return new List<object[]>
            {
                new object[] { new Delivery() {DeliveryPrice = int.MaxValue, DeliveryStatus = "", GoodsId = int.MaxValue, Id = int.MaxValue, UserId = int.MaxValue } },
                 
            };
        }
        [Fact]
        public async Task CreateAsyncNewDeliveryShouldCreateNewDelivery()
        {
            var newDelivery = new Delivery()
            {
                DeliveryStatus = "a",
                DeliveryPrice = int.MaxValue,
                GoodsId = int.MaxValue,
                Id = int.MaxValue, 
                UserId = int.MaxValue

            };
            await service.Create(newDelivery);
            deliveryRepositoryMoq.Verify(x => x.Create(It.IsAny<Delivery>()), Times.Once);
        }
    }
}
