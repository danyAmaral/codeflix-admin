using Bogus;
using Codeflix.Catalog.Application.UseCases.Category.ListCategories;
using Codeflix.Catalog.Domain.Entity;
using Codeflix.Catalog.Domain.SeedWork;
using Codeflix.Catalog.UnitTests.Domain.Entity.Category;

namespace Codeflix.Catalog.UnitTests.Application.ListCategories
{
    [CollectionDefinition(nameof(ListCategoriesTestFixture))]
    public class ListCategoriesTestFixtureCollection
     : ICollectionFixture<ListCategoriesTestFixture>
    { }

    public class ListCategoriesTestFixture
        : CategoryTestFixture
    {
        public List<Category> GetExampleCategoriesList(int length = 10)
        {
            var list = new List<Category>();
            for (int i = 0; i < length; i++)
                list.Add(GetValidCategory());
            return list;
        }

        public ListCategoriesRequest GetExampleInput()
        {
            var random = new Random();
            return new ListCategoriesRequest(
                page: random.Next(1, 10),
                perPage: random.Next(15, 100),
                search: Faker.Commerce.ProductName(),
                sort: Faker.Commerce.ProductName(),
                dir: random.Next(0, 10) > 5 ?
                    SearchOrder.Asc : SearchOrder.Desc
            );
        }
    }
}
