﻿using Codeflix.Catalog.Application.Interfaces;
using Codeflix.Catalog.Domain.Repository;

namespace Codeflix.Catalog.Application.UseCases.Category.DeleteCategory
{
    public class DeleteCategory : IDeleteCategory
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteCategory(ICategoryRepository categoryRepository, IUnitOfWork unitOfWork)
            => (_categoryRepository, _unitOfWork) = (categoryRepository, unitOfWork);

        public async Task Handle(DeleteCategoryRequest request, CancellationToken cancellationToken)
        {
            var category = await _categoryRepository.Get(request.Id, cancellationToken);
            await _categoryRepository.Delete(category, cancellationToken);
            await _unitOfWork.Commit(cancellationToken);
        }
    }
}
