using Codeflix.Catalog.Application.Interfaces;
using Codeflix.Catalog.Application.UseCases.Category.CreateCategory;
using Codeflix.Catalog.Domain.Entity;
using Codeflix.Catalog.Domain.Repository;
using FluentAssertions;
using Moq;
using UseCases = Codeflix.Catalog.Application.UseCases.Category.CreateCategory;

namespace Codeflix.Catalog.UnitTests.Application.CreateCategory
{
    public class CreateCategoryTest
    {
        [Fact(DisplayName = nameof(CreateCategory))]
        [Trait("Application", "CreateCategory - use cases")]
        public async void CreateCategory()
        {
            var repositoryMock = new Mock<ICategoryRepository>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var userCase = new UseCases.CreateCategory(unitOfWorkMock.Object, repositoryMock.Object);

            var request = new CreateCategoryRequest("category name", "Caterory Description", true);

            var response = await userCase.Handle(request, CancellationToken.None);

            repositoryMock.Verify(repository => repository.Insert(It.IsAny<Category>(), It.IsAny<CancellationToken>()), Times.Once());
            unitOfWorkMock.Verify(unitOfWork => unitOfWork.Commit(It.IsAny<CancellationToken>()), Times.Once());

            response.Should().NotBeNull();
            response.Name.Should().Be(request.Name);
            response.Id.Should().NotBeEmpty();
            response.CreatedAt.Should().NotBeSameDateAs(default);
        }
    }
}
