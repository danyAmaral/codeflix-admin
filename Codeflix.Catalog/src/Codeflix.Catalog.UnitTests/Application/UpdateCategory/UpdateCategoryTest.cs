﻿using Codeflix.Catalog.Application.Exceptions;
using Codeflix.Catalog.Application.UseCases.Category.Common;
using Codeflix.Catalog.Application.UseCases.Category.UpdateCategory;
using Codeflix.Catalog.Domain.Exeptions;
using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DomainEntity = Codeflix.Catalog.Domain.Entity;
using UseCase = Codeflix.Catalog.Application.UseCases.Category.UpdateCategory;

namespace Codeflix.Catalog.UnitTests.Application.UpdateCategory
{
    [Collection(nameof(UpdateCategoryTestFixture))]
    public class UpdateCategoryTest
    {
        private readonly UpdateCategoryTestFixture _fixture;

        public UpdateCategoryTest(UpdateCategoryTestFixture fixture)
            => _fixture = fixture;

        [Theory(DisplayName = nameof(UpdateCategory))]
        [Trait("Application", "UpdateCategory - Use Cases")]
        [MemberData(
            nameof(UpdateCategoryTestDataGenerator.GetCategoriesToUpdate),
            parameters: 10,
            MemberType = typeof(UpdateCategoryTestDataGenerator)
        )]
        public async Task UpdateCategory(
            DomainEntity.Category exampleCategory,
            UpdateCategoryRequest input
        )
        {
            var repositoryMock = _fixture.GetCategoryRepositoryMock();
            var unitOfWorkMock = _fixture.GetUnitOfWorkMock();
            repositoryMock.Setup(x => x.Get(
                exampleCategory.Id,
                It.IsAny<CancellationToken>())
            ).ReturnsAsync(exampleCategory);
            var useCase = new UseCase.UpdateCategory(
                repositoryMock.Object,
                unitOfWorkMock.Object
            );

            CategoryModelResponse output = await useCase.Handle(input, CancellationToken.None);

            output.Should().NotBeNull();
            output.Name.Should().Be(input.Name);
            output.Description.Should().Be(input.Description);
            output.IsActive.Should().Be((bool)input.IsActive!);
            repositoryMock.Verify(x => x.Get(
                exampleCategory.Id,
                It.IsAny<CancellationToken>())
            , Times.Once);
            repositoryMock.Verify(x => x.Update(
                exampleCategory,
                It.IsAny<CancellationToken>())
            , Times.Once);
            unitOfWorkMock.Verify(x => x.Commit(
                It.IsAny<CancellationToken>()),
                Times.Once
            );
        }

        [Theory(DisplayName = nameof(UpdateCategoryWithoutProvidingIsActive))]
        [Trait("Application", "UpdateCategory - Use Cases")]
        [MemberData(
            nameof(UpdateCategoryTestDataGenerator.GetCategoriesToUpdate),
            parameters: 10,
            MemberType = typeof(UpdateCategoryTestDataGenerator)
        )]
        public async Task UpdateCategoryWithoutProvidingIsActive(
            DomainEntity.Category exampleCategory,
            UpdateCategoryRequest exampleInput
        )
        {
            var input = new UpdateCategoryRequest(
                exampleInput.Id,
                exampleInput.Name,
                exampleInput.Description
            );
            var repositoryMock = _fixture.GetCategoryRepositoryMock();
            var unitOfWorkMock = _fixture.GetUnitOfWorkMock();
            repositoryMock.Setup(x => x.Get(
                exampleCategory.Id,
                It.IsAny<CancellationToken>())
            ).ReturnsAsync(exampleCategory);
            var useCase = new UseCase.UpdateCategory(
                repositoryMock.Object,
                unitOfWorkMock.Object
            );

            CategoryModelResponse output = await useCase.Handle(input, CancellationToken.None);

            output.Should().NotBeNull();
            output.Name.Should().Be(input.Name);
            output.Description.Should().Be(input.Description);
            output.IsActive.Should().Be(exampleCategory.IsActive);
            repositoryMock.Verify(x => x.Get(
                exampleCategory.Id,
                It.IsAny<CancellationToken>())
            , Times.Once);
            repositoryMock.Verify(x => x.Update(
                exampleCategory,
                It.IsAny<CancellationToken>())
            , Times.Once);
            unitOfWorkMock.Verify(x => x.Commit(
                It.IsAny<CancellationToken>()),
                Times.Once
            );
        }


        [Theory(DisplayName = nameof(UpdateCategoryOnlyName))]
        [Trait("Application", "UpdateCategory - Use Cases")]
        [MemberData(
            nameof(UpdateCategoryTestDataGenerator.GetCategoriesToUpdate),
            parameters: 10,
            MemberType = typeof(UpdateCategoryTestDataGenerator)
        )]
        public async Task UpdateCategoryOnlyName(
            DomainEntity.Category exampleCategory,
            UpdateCategoryRequest exampleInput
        )
        {
            var input = new UpdateCategoryRequest(
                exampleInput.Id,
                exampleInput.Name
            );
            var repositoryMock = _fixture.GetCategoryRepositoryMock();
            var unitOfWorkMock = _fixture.GetUnitOfWorkMock();
            repositoryMock.Setup(x => x.Get(
                exampleCategory.Id,
                It.IsAny<CancellationToken>())
            ).ReturnsAsync(exampleCategory);
            var useCase = new UseCase.UpdateCategory(
                repositoryMock.Object,
                unitOfWorkMock.Object
            );

            CategoryModelResponse output = await useCase.Handle(input, CancellationToken.None);

            output.Should().NotBeNull();
            output.Name.Should().Be(input.Name);
            output.Description.Should().Be(exampleCategory.Description);
            output.IsActive.Should().Be(exampleCategory.IsActive);
            repositoryMock.Verify(x => x.Get(
                exampleCategory.Id,
                It.IsAny<CancellationToken>())
            , Times.Once);
            repositoryMock.Verify(x => x.Update(
                exampleCategory,
                It.IsAny<CancellationToken>())
            , Times.Once);
            unitOfWorkMock.Verify(x => x.Commit(
                It.IsAny<CancellationToken>()),
                Times.Once
            );
        }

        [Fact(DisplayName = nameof(ThrowWhenCategoryNotFound))]
        [Trait("Application", "UpdateCategory - Use Cases")]
        public async Task ThrowWhenCategoryNotFound()
        {
            var repositoryMock = _fixture.GetCategoryRepositoryMock();
            var unitOfWorkMock = _fixture.GetUnitOfWorkMock();
            var input = _fixture.GetValidInput();
            repositoryMock.Setup(x => x.Get(
                input.Id,
                It.IsAny<CancellationToken>())
            ).ThrowsAsync(new NotFoundException($"Category '{input.Id}' not found"));
            var useCase = new UseCase.UpdateCategory(
                repositoryMock.Object,
                unitOfWorkMock.Object
            );

            var task = async ()
                => await useCase.Handle(input, CancellationToken.None);

            await task.Should().ThrowAsync<NotFoundException>();

            repositoryMock.Verify(x => x.Get(
                input.Id,
                It.IsAny<CancellationToken>())
            , Times.Once);
        }

        [Theory(DisplayName = nameof(ThrowWhenCantUpdateCategory))]
        [Trait("Application", "UpdateCategory - Use Cases")]
        [MemberData(
            nameof(UpdateCategoryTestDataGenerator.GetInvalidInputs),
            parameters: 12,
            MemberType = typeof(UpdateCategoryTestDataGenerator)
        )]
        public async Task ThrowWhenCantUpdateCategory(
            UpdateCategoryRequest input,
            string expectedExceptionMessage
        )
        {
            var exampleCategory = _fixture.GetValidCategory();
            input.Id = exampleCategory.Id;
            var repositoryMock = _fixture.GetCategoryRepositoryMock();
            var unitOfWorkMock = _fixture.GetUnitOfWorkMock();
            repositoryMock.Setup(x => x.Get(
                exampleCategory.Id,
                It.IsAny<CancellationToken>())
            ).ReturnsAsync(exampleCategory);
            var useCase = new UseCase.UpdateCategory(
                repositoryMock.Object,
                unitOfWorkMock.Object
            );

            var task = async ()
                => await useCase.Handle(input, CancellationToken.None);

            await task.Should().ThrowAsync<EntityValidationException>()
                .WithMessage(expectedExceptionMessage);

            repositoryMock.Verify(x => x.Get(
                exampleCategory.Id,
                It.IsAny<CancellationToken>()),
            Times.Once);
        }
    }
}
