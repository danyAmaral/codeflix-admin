using Codeflix.Catalog.Application.Common;
using Codeflix.Catalog.Application.UseCases.Category.Common;

namespace Codeflix.Catalog.Application.UseCases.Category.ListCategories
{
    public class ListCategoriesResponse
     : PaginatedListResponse<CategoryModelResponse>
    {
        public ListCategoriesResponse(
            int page,
            int perPage,
            int total,
            IReadOnlyList<CategoryModelResponse> items)
            : base(page, perPage, total, items)
        {
        }
    }
}
