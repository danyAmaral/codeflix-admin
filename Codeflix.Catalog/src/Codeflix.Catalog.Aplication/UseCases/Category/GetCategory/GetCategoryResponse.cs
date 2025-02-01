﻿using DomainEntity = Codeflix.Catalog.Domain.Entity;

namespace Codeflix.Catalog.Application.UseCases.Category.GetCategory
{
    public class GetCategoryResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }

        public GetCategoryResponse(Guid id, string name, string? description, bool isActive, DateTime createdAt)
        {
            Id = id;
            Name = name;
            Description = description;
            IsActive = isActive;
            CreatedAt = createdAt;
        }

        public static GetCategoryResponse FromCategory(DomainEntity.Category category)
        {
            return new GetCategoryResponse(category.Id, category.Name, category.Description, category.IsActive, category.CreatedAt);
        }

    }
}
