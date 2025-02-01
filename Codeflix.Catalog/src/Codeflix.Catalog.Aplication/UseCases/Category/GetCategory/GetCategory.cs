using Codeflix.Catalog.Domain.Repository;
using MediatR;

namespace Codeflix.Catalog.Application.UseCases.Category.GetCategory
{
    public class GetCategory : IRequestHandler<GetCategoryRequest, GetCategoryResponse>
    {
        private readonly ICategoryRepository _categoryRepository;

        public GetCategory(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
        public async Task<GetCategoryResponse> Handle(GetCategoryRequest request, CancellationToken cancellationToken)
        {
            var category = await _categoryRepository.Get(request.Id, cancellationToken);

            return GetCategoryResponse.FromCategory(category);
        }
    }
}
