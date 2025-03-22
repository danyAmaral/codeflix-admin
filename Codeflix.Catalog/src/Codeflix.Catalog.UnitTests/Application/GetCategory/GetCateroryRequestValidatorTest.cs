using Codeflix.Catalog.Application.UseCases.Category.GetCategory;
using FluentAssertions;
using FluentValidation;

namespace Codeflix.Catalog.UnitTests.Application.GetCategory
{
    [Collection(nameof(GetCategoryTestFixture))]
    public class GetCateroryRequestValidatorTest
    {
        private readonly GetCategoryTestFixture fixture;

        public GetCateroryRequestValidatorTest(GetCategoryTestFixture fixture)
        {
            this.fixture = fixture;
        }

        [Fact(DisplayName = nameof(ValidationOK))]
        [Trait("Aplication", "GetCateroryInputValidatorTest - UseCases")]
        public void ValidationOK()
        {
            var requestValid = new GetCategoryRequest(Guid.NewGuid());
            var validator = new GetCateroryRequestValidator();
            var validatorResult = validator.Validate(requestValid);

            validatorResult.Should().NotBeNull();
            validatorResult.IsValid.Should().BeTrue();
            validatorResult.Errors.Should().HaveCount(0);
        }

        [Fact(DisplayName = nameof(InvalidWhenEmptyGuidId))]
        [Trait("Application", "GetCateroryInputValidatorTest - UseCases")]
        public void InvalidWhenEmptyGuidId()
        {
            var requestInvalid = new GetCategoryRequest(Guid.Empty);
            var validator = new GetCateroryRequestValidator();

            var validatorResult = validator.Validate(requestInvalid);

            validatorResult.Should().NotBeNull();
            validatorResult.IsValid.Should().BeFalse();
            validatorResult.Errors.Should().HaveCount(1);
        }
    }
}
