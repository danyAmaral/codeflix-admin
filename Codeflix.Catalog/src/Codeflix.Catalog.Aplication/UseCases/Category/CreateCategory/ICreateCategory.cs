namespace Codeflix.Catalog.Application.UseCases.Category.CreateCategory
{
    public interface ICreateCategory
    {
        public Task<CreateCategoryResponse> Handle(CreateCategoryRequest request, CancellationToken cancellationToken);
    }
}
