namespace Codeflix.Catalog.Domain.SeedWork
{
    public interface ISearchableRepository<Taggregate>
    where Taggregate : AggregateRoot
    {
        Task<SearchResponse<Taggregate>> Search(
            SearchRequest input,
            CancellationToken cancellationToken
        );
    }
}
