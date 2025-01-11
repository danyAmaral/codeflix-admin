using Codeflix.Catalog.Application.Interfaces;
using Codeflix.Catalog.Domain.Repository;

namespace Codeflix.Catalog.Application.UseCases.Category.CreateCategory
{
    public class CreateCategory : ICreateCategory
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICategoryRepository _categoryRepository;

        public CreateCategory(IUnitOfWork unitOfWork, ICategoryRepository categoryRepository)
        {
            _unitOfWork = unitOfWork;
            _categoryRepository = categoryRepository;
        }

        public async Task<CreateCategoryResponse> Handle(CreateCategoryRequest request, CancellationToken cancellationToken)
        {
            var category = new Domain.Entity.Category(request.Name, request.Description, request.IsActive);

            await _categoryRepository.Insert(category, cancellationToken);
            await _unitOfWork.Commit(cancellationToken);

            var response = new CreateCategoryResponse(category.Id, category.Name, category.Description, category.IsActive, category.CreatedAt);

            return response;
        }
    }
}
