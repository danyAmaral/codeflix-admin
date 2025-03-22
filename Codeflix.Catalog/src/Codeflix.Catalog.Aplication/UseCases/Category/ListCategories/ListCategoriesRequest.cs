using Codeflix.Catalog.Application.Common;
using Codeflix.Catalog.Domain.SeedWork;
using MediatR;

namespace Codeflix.Catalog.Application.UseCases.Category.ListCategories
{
    public class ListCategoriesRequest
    : PaginatedListRequest, IRequest<ListCategoriesResponse>
    {
        public ListCategoriesRequest(
            int page = 1,
            int perPage = 15,
            string search = "",
            string sort = "",
            SearchOrder dir = SearchOrder.Asc
        ) : base(page, perPage, search, sort, dir)
        { }

        public ListCategoriesRequest()
            : base(1, 15, "", "", SearchOrder.Asc)
        { }
    }
}
