﻿namespace Codeflix.Catalog.Domain.SeedWork
{
    public class SearchRequest
    {
        public int Page { get; set; }
        public int PerPage { get; set; }
        public string Search { get; set; }
        public string OrderBy { get; set; }
        public SearchOrder Order { get; set; }

        public SearchRequest(
            int page,
            int perPage,
            string search,
            string orderBy,
            SearchOrder order)
        {
            Page = page;
            PerPage = perPage;
            Search = search;
            OrderBy = orderBy;
            Order = order;
        }
    }
}
