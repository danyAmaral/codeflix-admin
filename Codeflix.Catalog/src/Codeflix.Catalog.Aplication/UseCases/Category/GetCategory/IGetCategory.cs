using Codeflix.Catalog.Application.UseCases.Category.Common;
using Codeflix.Catalog.Application.UseCases.Category.CreateCategory;
using MediatR;

namespace Codeflix.Catalog.Application.UseCases.Category.GetCategory
{
    public interface IGetCategory : IRequestHandler<GetCategoryRequest, CategoryModelResponse>
    {
        public Task<CategoryModelResponse> Handle(GetCategoryRequest request, CancellationToken cancellationToken);
    }
}
