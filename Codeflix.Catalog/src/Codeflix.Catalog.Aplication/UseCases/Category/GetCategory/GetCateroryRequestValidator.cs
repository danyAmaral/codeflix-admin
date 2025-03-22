using FluentValidation;

namespace Codeflix.Catalog.Application.UseCases.Category.GetCategory
{
    public class GetCateroryRequestValidator : AbstractValidator<GetCategoryRequest>
    {
        public GetCateroryRequestValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
        }
    }
}
