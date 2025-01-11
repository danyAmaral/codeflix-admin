using Codeflix.Catalog.Domain.Exeptions;
using FluentAssertions;
using DomainEntity = Codeflix.Catalog.Domain.Entity;
namespace Codeflix.Catalog.UnitTests.Domain.Entity.Category
{
    [Collection(nameof(CategoryTestFixture))]
    public class CategoryTest
    {
        private CategoryTestFixture _categoryFixture;

        public CategoryTest(CategoryTestFixture categoryTestFixture)
        {
            _categoryFixture = categoryTestFixture;
        }

        [Fact(DisplayName = nameof(Instantiate))]
        [Trait("Domain", "Category - Agregates")]
        public void Instantiate()
        {
            var validData = _categoryFixture.GetValidCategory();

            var datetimeBefore = DateTime.Now;

            var category = new DomainEntity.Category(validData.Name, validData.Description);

            var datetimeAfter = DateTime.Now;

            category.Should().NotBeNull();
            category.Name.Should().Be(validData.Name);
            category.Description.Should().Be(validData.Description);
            category.Id.Should().NotBe(default(Guid));
            category.CreatedAt.Should().NotBeSameDateAs(default(DateTime));
            (category.CreatedAt >= datetimeBefore).Should().BeTrue();
            (category.CreatedAt <= datetimeAfter).Should().BeTrue();
            category.IsActive.Should().BeTrue();  
        }


        [Theory(DisplayName = nameof(InstantiateWithIsActive))]
        [Trait("Domain", "Category - Aggregates")]
        [InlineData(true)]
        [InlineData(false)]
        public void InstantiateWithIsActive(bool isActive)
        {
            var validCategory = _categoryFixture.GetValidCategory();

            var datetimeBefore = DateTime.Now;

            var category = new DomainEntity.Category(validCategory.Name, validCategory.Description, isActive);

            var datetimeAfter = DateTime.Now;


            category.Should().NotBeNull();
            category.Name.Should().Be(validCategory.Name);
            category.Description.Should().Be(validCategory.Description);
            category.Id.Should().NotBeEmpty();
            category.CreatedAt.Should().NotBeSameDateAs(default);
            (category.CreatedAt >= datetimeBefore).Should().BeTrue();
            (category.CreatedAt <= datetimeAfter).Should().BeTrue();
            (category.IsActive).Should().Be(isActive);
        }


        [Theory(DisplayName = nameof(InstantiateErrorWhenNameIsEmpty))]
        [Trait("Domain", "Category - Aggregates")]
        [InlineData("")]
        [InlineData(null)]
        [InlineData("   ")]
        public void InstantiateErrorWhenNameIsEmpty(string? name)
        {
            Action action = () => new DomainEntity.Category(name!, "Description");
            action.Should()
            .Throw<EntityValidationException>()
            .WithMessage("Name should not be empty or null");
        }


        [Fact(DisplayName = nameof(InstantiateErrorWhenDescriptionIsNull))]
        [Trait("Domain", "Category - Aggregates")]      
        public void InstantiateErrorWhenDescriptionIsNull()
        {
            Action action = () => new DomainEntity.Category("Name", null!);

            action.Should()
                .Throw<EntityValidationException>()
                .WithMessage("Description should not be null");
        }


        [Theory(DisplayName = nameof(InstantiateErrorWhenNameIsLessThan3Characters))]
        [Trait("Domain", "Category - Aggregates")]
        [MemberData(nameof(GetNamesWithLessThan3Characters), parameters: 10)]
        public void InstantiateErrorWhenNameIsLessThan3Characters(string invalidName)
        {
            var validCategory = _categoryFixture.GetValidCategory();

            Action action =
                () => new DomainEntity.Category(invalidName, validCategory.Description);

            action.Should()
                .Throw<EntityValidationException>()
                .WithMessage("Name should be at least 3 characters long");
        }

        public static IEnumerable<object[]> GetNamesWithLessThan3Characters(int numberOfTests = 6)
        {
            var fixture = new CategoryTestFixture();

            for (int i = 0; i < numberOfTests; i++)
            {
                var isOdd = i % 2 == 1;
                yield return new object[] { fixture.GetValidCategoryName()[..(isOdd ? 1 : 2)] };
            }
        }


        [Fact(DisplayName = nameof(UpdateErrorWhenNameIsGreaterThan255Characters))]
        [Trait("Domain", "Category - Aggregates")]
        public void UpdateErrorWhenNameIsGreaterThan255Characters()
        {
            var category = _categoryFixture.GetValidCategory();
            var invalidName = _categoryFixture.Faker.Lorem.Letter(256);

            Action action =
                () => category.Update(invalidName);

            action.Should()
                .Throw<EntityValidationException>()
                .WithMessage("Name should be less or equal 255 characters long");
        }


        [Fact(DisplayName = nameof(UpdateErrorWhenDescriptionIsGreaterThan10_000Characters))]
        [Trait("Domain", "Category - Aggregates")]
        public void UpdateErrorWhenDescriptionIsGreaterThan10_000Characters()
        {
            var category = _categoryFixture.GetValidCategory();
            var invalidDescription =
                _categoryFixture.Faker.Commerce.ProductDescription();
            while (invalidDescription.Length <= 10_000)
                invalidDescription = $"{invalidDescription} {_categoryFixture.Faker.Commerce.ProductDescription()}";

            Action action =
                () => category.Update("Category New Name", invalidDescription);

            action.Should()
                .Throw<EntityValidationException>()
                .WithMessage("Description should be less or equal 10000 characters long");
        }

        [Fact(DisplayName = nameof(Activate))]
        [Trait("Domain", "Category - Aggregates")]
        public void Activate()
        {
           var validCategory = _categoryFixture.GetValidCategory();

            var category = new DomainEntity.Category(validCategory.Name, validCategory.Description, false);
            category.Activate();

            category.IsActive.Should().BeTrue();
        }

        [Fact(DisplayName = nameof(Inactivate))]
        [Trait("Domain", "Category - Aggregates")]
        public void Inactivate()
        {
            var validCategory = _categoryFixture.GetValidCategory();

            var category = new DomainEntity.Category(validCategory.Name, validCategory.Description, true);
            category.Deactivate();

            category.IsActive.Should().BeFalse();
        }

        [Fact(DisplayName = nameof(Update))]
        [Trait("Domain", "Category - Aggregates")]
        public void Update()
        {
            var validCategory = _categoryFixture.GetValidCategory();

            var category = new DomainEntity.Category(validCategory.Name, validCategory.Description, true);

            var newValues = new { Name = "New Name", Description = "New Description"};

            category.Update(newValues.Name, newValues.Description);

            category.Name.Should().BeEquivalentTo(newValues.Name);
            category.Description.Should().BeEquivalentTo(newValues.Description);
        }

        [Fact(DisplayName = nameof(UpdateOnlyName))]
        [Trait("Domain", "Category - Aggregates")]
        public void UpdateOnlyName()
        {
            var validCategory = _categoryFixture.GetValidCategory();

            var category = new DomainEntity.Category(validCategory.Name, validCategory.Description, true);

            var newValues = new { Name = "New Name" };

            category.Update(newValues.Name);

            category.Name.Should().BeEquivalentTo(newValues.Name); 
        }

        [Theory(DisplayName = nameof(UpdateErrorWhenNameIsLessThan3Characters))]
        [Trait("Domain", "Category - Aggregates")]
        [InlineData("1")]
        [InlineData("12")]
        [InlineData("a")]
        [InlineData("ca")]
        public void UpdateErrorWhenNameIsLessThan3Characters(string invalidName)
        {
            var category = _categoryFixture.GetValidCategory();

            Action action =
                () => category.Update(invalidName);

            action.Should()
                .Throw<EntityValidationException>()
                .WithMessage("Name should be at least 3 characters long");
        }

        [Fact(DisplayName = nameof(UpdateOnlyDescription))]
        [Trait("Domain", "Category - Aggregates")]
        public void UpdateOnlyDescription()
        {
            var validCategory = _categoryFixture.GetValidCategory();

            var category = new DomainEntity.Category(validCategory.Name, validCategory.Description, true);

            var newValues = new { Desciption = "New Description" };

            category.Update(null, newValues.Desciption);

            category.Description.Should().BeEquivalentTo(newValues.Desciption);
        }
    }
}
