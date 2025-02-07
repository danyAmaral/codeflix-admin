using Codeflix.Catalog.Application.UseCases.Category.Common;
using MediatR;

namespace Codeflix.Catalog.Application.UseCases.Category.CreateCategory
{
    public interface ICreateCategory: IRequestHandler<CreateCategoryRequest, CategoryModelResponse>
    {
        public Task<CategoryModelResponse> Handle(CreateCategoryRequest request, CancellationToken cancellationToken);
    }
}
