﻿using Codeflix.Catalog.Application.UseCases.Category.Common;
using DomainEntity = Codeflix.Catalog.Domain.Entity;
using Moq;
using Codeflix.Catalog.Domain.SeedWork;

using UseCase = Codeflix.Catalog.Application.UseCases.Category.ListCategories;
using FluentAssertions;

namespace Codeflix.Catalog.UnitTests.Application.ListCategories
{
    [Collection(nameof(ListCategoriesTestFixture))]
    public class ListCategoriesTest
    {
        private readonly ListCategoriesTestFixture _fixture;

        public ListCategoriesTest(ListCategoriesTestFixture fixture)
            => _fixture = fixture;

        [Fact(DisplayName = nameof(List))]
        [Trait("Application", "ListCategories - Use Cases")]
        public async Task List()
        {
            var categoriesExampleList = _fixture.GetExampleCategoriesList();
            var repositoryMock = _fixture.GetCategoryRepositoryMock();
            var input = _fixture.GetExampleInput();
            var outputRepositorySearch = new SearchResponse<DomainEntity.Category>(
                currentPage: input.Page,
                perPage: input.PerPage,
                items: (IReadOnlyList<DomainEntity.Category>)categoriesExampleList,
                total: new Random().Next(50, 200)
            );
            repositoryMock.Setup(x => x.Search(
                It.Is<SearchRequest>(
                    searchInput => searchInput.Page == input.Page
                    && searchInput.PerPage == input.PerPage
                    && searchInput.Search == input.Search
                    && searchInput.OrderBy == input.Sort
                    && searchInput.Order == input.Dir
                ),
                It.IsAny<CancellationToken>()
            )).ReturnsAsync(outputRepositorySearch);
            var useCase = new UseCase.ListCategories(repositoryMock.Object);

            var output = await useCase.Handle(input, CancellationToken.None);

            output.Should().NotBeNull();
            output.Page.Should().Be(outputRepositorySearch.CurrentPage);
            output.PerPage.Should().Be(outputRepositorySearch.PerPage);
            output.Total.Should().Be(outputRepositorySearch.Total);
            output.Items.Should().HaveCount(outputRepositorySearch.Items.Count);
            ((List<CategoryModelResponse>)output.Items).ForEach(outputItem =>
            {
                var repositoryCategory = outputRepositorySearch.Items
                    .FirstOrDefault(x => x.Id == outputItem.Id);
                outputItem.Should().NotBeNull();
                outputItem.Name.Should().Be(repositoryCategory!.Name);
                outputItem.Description.Should().Be(repositoryCategory!.Description);
                outputItem.IsActive.Should().Be(repositoryCategory!.IsActive);
                outputItem.CreatedAt.Should().Be(repositoryCategory!.CreatedAt);
            });
            repositoryMock.Verify(x => x.Search(
                It.Is<SearchRequest>(
                    searchInput => searchInput.Page == input.Page
                    && searchInput.PerPage == input.PerPage
                    && searchInput.Search == input.Search
                    && searchInput.OrderBy == input.Sort
                    && searchInput.Order == input.Dir
                ),
                It.IsAny<CancellationToken>()
            ), Times.Once);
        }


        [Fact(DisplayName = nameof(ListOkWhenEmpty))]
        [Trait("Application", "ListCategories - Use Cases")]
        public async Task ListOkWhenEmpty()
        {
            var repositoryMock = _fixture.GetCategoryRepositoryMock();
            var input = _fixture.GetExampleInput();
            var outputRepositorySearch = new SearchResponse<DomainEntity.Category>(
                currentPage: input.Page,
                perPage: input.PerPage,
                items: new List<DomainEntity.Category>().AsReadOnly(),
                total: 0
            );
            repositoryMock.Setup(x => x.Search(
                It.Is<SearchRequest>(
                    searchInput => searchInput.Page == input.Page
                    && searchInput.PerPage == input.PerPage
                    && searchInput.Search == input.Search
                    && searchInput.OrderBy == input.Sort
                    && searchInput.Order == input.Dir
                ),
                It.IsAny<CancellationToken>()
            )).ReturnsAsync(outputRepositorySearch);
            var useCase = new UseCase.ListCategories(repositoryMock.Object);

            var output = await useCase.Handle(input, CancellationToken.None);

            output.Should().NotBeNull();
            output.Page.Should().Be(outputRepositorySearch.CurrentPage);
            output.PerPage.Should().Be(outputRepositorySearch.PerPage);
            output.Total.Should().Be(0);
            output.Items.Should().HaveCount(0);

            repositoryMock.Verify(x => x.Search(
                It.Is<SearchRequest>(
                    searchInput => searchInput.Page == input.Page
                    && searchInput.PerPage == input.PerPage
                    && searchInput.Search == input.Search
                    && searchInput.OrderBy == input.Sort
                    && searchInput.Order == input.Dir
                ),
                It.IsAny<CancellationToken>()
            ), Times.Once);
        }

        [Theory(DisplayName = nameof(ListInputWithoutAllParameters))]
        [Trait("Application", "ListCategories - Use Cases")]
        [MemberData(
            nameof(ListCategoriesTestDataGenerator.GetInputsWithoutAllParameter),
            parameters: 14,
            MemberType = typeof(ListCategoriesTestDataGenerator)
        )]
        public async Task ListInputWithoutAllParameters(
            UseCase.ListCategoriesRequest input
        )
        {
            var categoriesExampleList = _fixture.GetExampleCategoriesList();
            var repositoryMock = _fixture.GetCategoryRepositoryMock();
            var outputRepositorySearch = new SearchResponse<DomainEntity.Category>(
                currentPage: input.Page,
                perPage: input.PerPage,
                items: (IReadOnlyList<DomainEntity.Category>)categoriesExampleList,
                total: new Random().Next(50, 200)
            );
            repositoryMock.Setup(x => x.Search(
                It.Is<SearchRequest>(
                    searchInput => searchInput.Page == input.Page
                    && searchInput.PerPage == input.PerPage
                    && searchInput.Search == input.Search
                    && searchInput.OrderBy == input.Sort
                    && searchInput.Order == input.Dir
                ),
                It.IsAny<CancellationToken>()
            )).ReturnsAsync(outputRepositorySearch);
            var useCase = new UseCase.ListCategories(repositoryMock.Object);

            var output = await useCase.Handle(input, CancellationToken.None);

            output.Should().NotBeNull();
            output.Page.Should().Be(outputRepositorySearch.CurrentPage);
            output.PerPage.Should().Be(outputRepositorySearch.PerPage);
            output.Total.Should().Be(outputRepositorySearch.Total);
            output.Items.Should().HaveCount(outputRepositorySearch.Items.Count);
            ((List<CategoryModelResponse>)output.Items).ForEach(outputItem =>
            {
                var repositoryCategory = outputRepositorySearch.Items
                    .FirstOrDefault(x => x.Id == outputItem.Id);
                outputItem.Should().NotBeNull();
                outputItem.Name.Should().Be(repositoryCategory!.Name);
                outputItem.Description.Should().Be(repositoryCategory!.Description);
                outputItem.IsActive.Should().Be(repositoryCategory!.IsActive);
                outputItem.CreatedAt.Should().Be(repositoryCategory!.CreatedAt);
            });
            repositoryMock.Verify(x => x.Search(
                It.Is<SearchRequest>(
                    searchInput => searchInput.Page == input.Page
                    && searchInput.PerPage == input.PerPage
                    && searchInput.Search == input.Search
                    && searchInput.OrderBy == input.Sort
                    && searchInput.Order == input.Dir
                ),
                It.IsAny<CancellationToken>()
            ), Times.Once);
        }
    }
}
