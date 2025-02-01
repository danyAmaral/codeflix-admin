using MediatR;

namespace Codeflix.Catalog.Application.UseCases.Category.CreateCategory
{
    public interface ICreateCategory: IRequestHandler<CreateCategoryRequest, CreateCategoryResponse>
    {
        public Task<CreateCategoryResponse> Handle(CreateCategoryRequest request, CancellationToken cancellationToken);
    }
}
