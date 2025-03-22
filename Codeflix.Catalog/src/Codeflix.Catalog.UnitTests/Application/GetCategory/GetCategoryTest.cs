using UseCase = Codeflix.Catalog.Application.UseCases.Category.GetCategory;
using Moq;
using FluentAssertions;
using Codeflix.Catalog.Application.Exceptions;

namespace Codeflix.Catalog.UnitTests.Application.GetCategory
{
    [Collection(nameof(GetCategoryTestFixture))]
    public class GetCategoryTest
    {

        private readonly GetCategoryTestFixture _fixture;

        public GetCategoryTest(GetCategoryTestFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact(DisplayName = "GetCategory")]
        [Trait("Application", "GetCategory - Use Case")]
        public async Task GetCategory()
        {
            var repositoryMock = _fixture.GetCategoryRepositoryMock();
            var categoryMock = _fixture.GetValidCategory();

            repositoryMock.Setup(x=> x.Get(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).ReturnsAsync(categoryMock);

            var request = new UseCase.GetCategoryRequest(categoryMock.Id);

            var userCase = new UseCase.GetCategory(repositoryMock.Object);

            var response = await userCase.Handle(request, CancellationToken.None);

            repositoryMock.Verify(x => x.Get(It.IsAny<Guid>(), It.IsAny<CancellationToken>()), Times.Once);

            response.Should().NotBeNull();
            response.Name.Should().Be(categoryMock.Name);
            response.Id.Should().Be(categoryMock.Id);
            response.CreatedAt.Should().Be(categoryMock.CreatedAt);
        }


        [Fact(DisplayName = nameof(NotFoundExceptionWhenCategoryDoesNotExist))]
        [Trait("Application", "GetCategory - Use Case")]
        public async Task NotFoundExceptionWhenCategoryDoesNotExist()
        {
            var repositoryMock = _fixture.GetCategoryRepositoryMock();
            var categoryMock = _fixture.GetValidCategory();
            var uuid = Guid.NewGuid();

            repositoryMock.Setup(x => x.Get(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                .ThrowsAsync(new NotFoundException($"Category {uuid} not found."));

            var request = new UseCase.GetCategoryRequest(uuid);

            var userCase = new UseCase.GetCategory(repositoryMock.Object);

            var task = async() =>  await userCase.Handle(request, CancellationToken.None);

            await task.Should().ThrowAsync<NotFoundException>();

            repositoryMock.Verify(x => x.Get(It.IsAny<Guid>(), It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}
