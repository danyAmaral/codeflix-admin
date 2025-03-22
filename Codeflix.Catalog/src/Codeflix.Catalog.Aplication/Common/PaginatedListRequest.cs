using Codeflix.Catalog.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codeflix.Catalog.Application.Common
{
    public abstract class PaginatedListRequest
    {
        public int Page { get; set; }
        public int PerPage { get; set; }
        public string Search { get; set; }
        public string Sort { get; set; }
        public SearchOrder Dir { get; set; }
        public PaginatedListRequest(
            int page,
            int perPage,
            string search,
            string sort,
            SearchOrder dir)
        {
            Page = page;
            PerPage = perPage;
            Search = search;
            Sort = sort;
            Dir = dir;
        }

        public SearchRequest ToSearchInput()
            => new(Page, PerPage, Search, Sort, Dir);
    }
}
