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
    public class CategoryServiceTest
    {
        private readonly CategoryService service;
        private readonly Mock<ICategoryRepository> categoryRepositoryMoq;

        public CategoryServiceTest()
        {
            var repositoryWrapperMoq = new Mock<IRepositoryWrapper>();
            categoryRepositoryMoq = new Mock<ICategoryRepository>();
            repositoryWrapperMoq.Setup(x => x.Category).Returns(categoryRepositoryMoq.Object);

            service = new CategoryService(repositoryWrapperMoq.Object);

        }
        [Fact]
        public async Task CreateAsync_NullCategory_ShouldThrowNullArgumentExpection()
        {
            var ex = await Assert.ThrowsAnyAsync<ArgumentNullException>(() => service.Create(null));
            Assert.IsType<ArgumentNullException>(ex);
            categoryRepositoryMoq.Verify(x => x.Create(It.IsAny<Category>()), Times.Never);
        }
        [Theory]
        [MemberData(nameof(GetIncorrectCategories))]
        public async Task CreateAsyncNewCategoryShouldNotCreateNewCategory(Category category)
        {
            var newCategory = category;

            var ex = await Assert.ThrowsAnyAsync<ArgumentException>(() => service.Create(newCategory));
            categoryRepositoryMoq.Verify(x => x.Create(It.IsAny<Category>()), Times.Never);
            Assert.IsType<ArgumentException>(ex);

        }
        public static IEnumerable<object[]> GetIncorrectCategories()
        {
            return new List<object[]>
            {
                new object[] { new Category() {CategoryName = "", GoodsId = int.MaxValue, Id = int.MaxValue} },
                

            };
        }
        [Fact]
        public async Task CreateAsyncNewCategoryShouldCreateNewCategory()
        {
            var newCategory = new Category()
            {
                CategoryName = "a",
                GoodsId = int.MaxValue,
                Id = int.MaxValue



            };
            await service.Create(newCategory);
            categoryRepositoryMoq.Verify(x => x.Create(It.IsAny<Category>()), Times.Once);
        }
    }
}
