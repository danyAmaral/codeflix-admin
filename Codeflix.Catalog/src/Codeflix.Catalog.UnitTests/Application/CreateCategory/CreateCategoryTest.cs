using Codeflix.Catalog.Application.Interfaces;
using Codeflix.Catalog.Application.UseCases.Category.CreateCategory;
using Codeflix.Catalog.Domain.Entity;
using Codeflix.Catalog.Domain.Exeptions;
using Codeflix.Catalog.Domain.Repository;
using Codeflix.Catalog.UnitTests.Domain.Entity.Category;
using FluentAssertions;
using Moq;
using System.Threading.Tasks;
using UseCases = Codeflix.Catalog.Application.UseCases.Category.CreateCategory;

namespace Codeflix.Catalog.UnitTests.Application.CreateCategory
{
    [Collection(nameof(CreateCatororyTestFixture))]
    public class CreateCategoryTest
    {
        private readonly CreateCatororyTestFixture _fixture;

        public CreateCategoryTest(CreateCatororyTestFixture createCatororyTestFixture)
        {
            _fixture = createCatororyTestFixture;
        }


        [Fact(DisplayName = nameof(CreateCategory))]
        [Trait("Application", "CreateCategory - use cases")]
        public async void CreateCategory()
        {

            var unitOfWorkMock = _fixture.GetIUnitOfWorkMock();
            var repositoryMock = _fixture.GetCategoryRepositoryMock();

            var userCase = new UseCases.CreateCategory(unitOfWorkMock.Object, repositoryMock.Object);

            var request = _fixture.GetCreateCategoryRequest();

            var response = await userCase.Handle(request, CancellationToken.None);

            repositoryMock.Verify(repository => repository.Insert(It.IsAny<Category>(), It.IsAny<CancellationToken>()), Times.Once());
            unitOfWorkMock.Verify(unitOfWork => unitOfWork.Commit(It.IsAny<CancellationToken>()), Times.Once());

            response.Should().NotBeNull();
            response.Name.Should().Be(request.Name);
            response.Id.Should().NotBeEmpty();
            response.CreatedAt.Should().NotBeSameDateAs(default);
        }


        [Theory(DisplayName = nameof(ThrowWhenCantInstantieteAggregateAsync))]
        [Trait("Application", "CreateCategory - use cases")]
        [MemberData(nameof(GetInvalidRequests))]
        public async Task ThrowWhenCantInstantieteAggregateAsync(CreateCategoryRequest request, string exceptionMessage)
        {
            var unitOfWorkMock = _fixture.GetIUnitOfWorkMock();
            var repositoryMock = _fixture.GetCategoryRepositoryMock();

            var userCase = new UseCases.CreateCategory(unitOfWorkMock.Object, repositoryMock.Object);

            Func<Task> task = async() =>  await userCase.Handle(request, CancellationToken.None);

            await task.Should()
                    .ThrowAsync<EntityValidationException>()
                    .WithMessage(exceptionMessage);
        }

        public static IEnumerable<object[]> GetInvalidRequests()
        {
            var fixture = new CreateCatororyTestFixture();
            var invalidRequestList = new List<object[]>();

            var invalidRequestInputShortName = fixture.GetCreateCategoryRequest();
            invalidRequestInputShortName.Name = invalidRequestInputShortName.Name.Substring(0, 2);

            invalidRequestList.Add(new object[] {
                fixture.GetInvalidRequestShortName(),
                "Name should be at least 3 characters long" 
            });

            invalidRequestList.Add(new object[] {
                fixture.GetInvalidRequestTolongName(),
                "Name should be less or equal 255 characters long"
            });

            return invalidRequestList;
        }
    }
}
