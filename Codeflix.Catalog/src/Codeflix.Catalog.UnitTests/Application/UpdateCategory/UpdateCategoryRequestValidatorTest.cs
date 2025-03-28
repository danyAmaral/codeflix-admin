﻿using Codeflix.Catalog.Application.UseCases.Category.UpdateCategory;
using FluentAssertions;
using FluentValidation;

namespace Codeflix.Catalog.UnitTests.Application.UpdateCategory
{
    [Collection(nameof(UpdateCategoryTestFixture))]
    public class UpdateCategoryInputValidatorTest
    {
        private readonly UpdateCategoryTestFixture _fixture;

        public UpdateCategoryInputValidatorTest(UpdateCategoryTestFixture fixture)
            => _fixture = fixture;

        [Fact(DisplayName = nameof(DontValidateWhenEmptyGuid))]
        [Trait("Application", "UpdateCategoryRequestValidator - Use Cases")]
        public void DontValidateWhenEmptyGuid()
        {
            ValidatorOptions.Global.LanguageManager.Enabled = false;
            var request = _fixture.GetValidInput(Guid.Empty);
            var validator = new UpdateCategoryRequestValidator();

            var validateResult = validator.Validate(request);

            validateResult.Should().NotBeNull();
            validateResult.IsValid.Should().BeFalse();
            validateResult.Errors.Should().HaveCount(1);
            validateResult.Errors[0].ErrorMessage
                .Should().Be("'Id' must not be empty.");
        }


        [Fact(DisplayName = nameof(ValidateWhenValid))]
        [Trait("Application", "UpdateCategoryRequestValidator - Use Cases")]
        public void ValidateWhenValid()
        {
            var request = _fixture.GetValidInput();
            var validator = new UpdateCategoryRequestValidator();

            var validateResult = validator.Validate(request);

            validateResult.Should().NotBeNull();
            validateResult.IsValid.Should().BeTrue();
            validateResult.Errors.Should().HaveCount(0);
        }
    }
}
