using MediatR;

namespace Codeflix.Catalog.Application.UseCases.Category.GetCategory
{
    public class GetCategoryRequest: IRequest<GetCategoryResponse>
    {
        public Guid Id { get; set; }

        public GetCategoryRequest(Guid id)
        {
            Id = id;
        }
    }
}
