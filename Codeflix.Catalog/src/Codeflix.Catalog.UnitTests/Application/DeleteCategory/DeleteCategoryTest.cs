using Codeflix.Catalog.Application.Exceptions;
using FluentAssertions;
using Moq;
using UseCases = Codeflix.Catalog.Application.UseCases.Category.DeleteCategory;

namespace Codeflix.Catalog.UnitTests.Application.DeleteCategory
{
    [Collection(nameof(DeleteCategoryTestFixture))]
    public class DeleteCategoryTest
    {
        private readonly DeleteCategoryTestFixture _fixture;

        public DeleteCategoryTest(DeleteCategoryTestFixture fixture)
            => _fixture = fixture;

        [Fact(DisplayName = nameof(DeleteCategory))]
        [Trait("Application", "DeleteCategory - Use Cases")]
        public async Task DeleteCategory()
        {
            var repositoryMock = _fixture.GetCategoryRepositoryMock();
            var unitOfWorkMock = _fixture.GetUnitOfWorkMock();
            var categoryExample = _fixture.GetValidCategory();
            repositoryMock.Setup(x => x.Get(
                categoryExample.Id,
                It.IsAny<CancellationToken>())
            ).ReturnsAsync(categoryExample);
            var request = new UseCases.DeleteCategoryRequest(categoryExample.Id);
            var useCase = new UseCases.DeleteCategory(
                repositoryMock.Object,
                unitOfWorkMock.Object);

            await useCase.Handle(request, CancellationToken.None);

            repositoryMock.Verify(x => x.Get(
                categoryExample.Id,
                It.IsAny<CancellationToken>()
            ), Times.Once);
            repositoryMock.Verify(x => x.Delete(
                categoryExample,
                It.IsAny<CancellationToken>()
            ), Times.Once);
            unitOfWorkMock.Verify(x => x.Commit(
                It.IsAny<CancellationToken>()
            ), Times.Once);
        }


        [Fact(DisplayName = nameof(ThrowWhenCategoryNotFound))]
        [Trait("Application", "DeleteCategory - Use Cases")]
        public async Task ThrowWhenCategoryNotFound()
        {
            var repositoryMock = _fixture.GetCategoryRepositoryMock();
            var unitOfWorkMock = _fixture.GetUnitOfWorkMock();
            var exampleGuid = Guid.NewGuid();
            repositoryMock.Setup(x => x.Get(
                exampleGuid,
                It.IsAny<CancellationToken>())
            ).ThrowsAsync(
                new NotFoundException($"Category '{exampleGuid}' not found")
            );
            var input = new UseCases.DeleteCategoryRequest(exampleGuid);
            var useCase = new UseCases.DeleteCategory(
                repositoryMock.Object,
                unitOfWorkMock.Object);

            var task = async ()
                => await useCase.Handle(input, CancellationToken.None);

            await task.Should()
                .ThrowAsync<NotFoundException>();

            repositoryMock.Verify(x => x.Get(
                exampleGuid,
                It.IsAny<CancellationToken>()
            ), Times.Once);
        }
    }
}
