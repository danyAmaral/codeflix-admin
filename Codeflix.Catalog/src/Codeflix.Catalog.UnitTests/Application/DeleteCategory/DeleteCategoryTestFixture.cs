using Codeflix.Catalog.UnitTests.Common;
using Codeflix.Catalog.UnitTests.Domain.Entity.Category;

namespace Codeflix.Catalog.UnitTests.Application.DeleteCategory
{
    [CollectionDefinition(nameof(DeleteCategoryTestFixture))]
    public class DeleteCategoryTestFixtureCollection
        : ICollectionFixture<DeleteCategoryTestFixture>
    { }

    public class DeleteCategoryTestFixture
        : CategoryTestFixture
    { }
}
