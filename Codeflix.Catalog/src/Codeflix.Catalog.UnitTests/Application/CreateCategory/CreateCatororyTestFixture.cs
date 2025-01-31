using Codeflix.Catalog.Application.Interfaces;
using Codeflix.Catalog.Application.UseCases.Category.CreateCategory;
using Codeflix.Catalog.Domain.Repository;
using Codeflix.Catalog.UnitTests.Common;
using Moq;

namespace Codeflix.Catalog.UnitTests.Application.CreateCategory
{
    public class CreateCatororyTestFixture: BaseFixture
    {
        public string GetValidCategoryName()
        {
            var categoryName = "";
            while (categoryName.Length < 3)
                categoryName = Faker.Commerce.Categories(1)[0];
            if (categoryName.Length > 255)
                categoryName = categoryName[..255];

            return categoryName;
        }

        public string GetValidCategoryDescription()
        {
            var categoryDescription =
                Faker.Commerce.ProductDescription();
            if (categoryDescription.Length > 10_000)
                categoryDescription =
                    categoryDescription[..10_000];

            return categoryDescription;
        }

        public CreateCategoryRequest GetInvalidRequestShortName()
        {
            var invalidInputShortName = GetCreateCategoryRequest();
            invalidInputShortName.Name = invalidInputShortName.Name.Substring(0, 2);

            return invalidInputShortName;
        }
        public CreateCategoryRequest GetInvalidRequestTolongName()
        {
            var invalidInputTooLongName = GetCreateCategoryRequest();
            var tooLongNameForCategory = Faker.Commerce.ProductName();
            while (tooLongNameForCategory.Length <= 255)
                tooLongNameForCategory = $"{tooLongNameForCategory} {Faker.Commerce.ProductName()}";
            invalidInputTooLongName.Name = tooLongNameForCategory;
            return invalidInputTooLongName;
        }

        public bool GetRandomBoolean()
        {
            return (new Random()).NextDouble() < 0.5;
        }

        public CreateCategoryRequest GetCreateCategoryRequest()
        {
            return new CreateCategoryRequest(GetValidCategoryName(), GetValidCategoryDescription(), GetRandomBoolean());
        }


        public Mock<ICategoryRepository> GetCategoryRepositoryMock()
        {
            return new();
        }

        public Mock<IUnitOfWork> GetIUnitOfWorkMock()
        {
            return new();
        }
    }

    [CollectionDefinition(nameof(CreateCatororyTestFixture))]
    public class CreateCatororyTestFixtureCollection : ICollectionFixture<CreateCatororyTestFixture>
    {
    }
}
