﻿using Codeflix.Catalog.Application.UseCases.Category.Common;
using MediatR;

namespace Codeflix.Catalog.Application.UseCases.Category.GetCategory
{
    public class GetCategoryRequest: IRequest<CategoryModelResponse>
    {
        public Guid Id { get; set; }

        public GetCategoryRequest(Guid id)
        {
            Id = id;
        }
    }
}
