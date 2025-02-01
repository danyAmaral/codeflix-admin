using DomainEntity = Codeflix.Catalog.Domain.Entity;

namespace Codeflix.Catalog.Application.UseCases.Category
{
    public class CreateCategoryResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }

        public CreateCategoryResponse(Guid id, string name, string? description, bool isActive, DateTime createdAt)
        {
            Id = id;
            Name = name;
            Description = description;
            IsActive = isActive;
            CreatedAt = createdAt;
        }

        public static CreateCategoryResponse FromCategory(DomainEntity.Category category)
        {
            return new CreateCategoryResponse(category.Id, category.Name, category.Description, category.IsActive, category.CreatedAt);
        }

    }
}
