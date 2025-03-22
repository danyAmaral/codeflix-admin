using Codeflix.Catalog.Application.UseCases.Category.Common;
using Codeflix.Catalog.Application.UseCases.Category.UpdateCategory;
using MediatR;

namespace Codeflix.Catalog.Application.UseCases.Category
{
    public interface IUpdateCategory : IRequestHandler<UpdateCategoryRequest, CategoryModelResponse>
    {
    }
}
