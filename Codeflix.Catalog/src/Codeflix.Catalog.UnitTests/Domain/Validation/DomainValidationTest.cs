using Bogus;
using Codeflix.Catalog.Domain.Exeptions;
using Codeflix.Catalog.Domain.Validation;
using FluentAssertions;

namespace Codeflix.Catalog.UnitTests.Domain.Validation
{
    public class DomainValidationTest
    {
        private Faker Faker { get; set; } = new Faker();

        [Fact(DisplayName = "NotNullOK")]
        [Trait("Domain", "DomainValidation - Validation")]
        public void NotNullOK()
        {
            var value = Faker.Commerce.ProductName();
            Action action = () => DomainValidation.NotNull(value, "value");
            action.Should().NotThrow();

        }

        [Fact(DisplayName = "NotNullThrowWhenNull")]
        [Trait("Domain", "DomainValidation - Validation")]
        public void NotNullThrowWhenNull()
        {
            string? value = null;
            Action action = () => DomainValidation.NotNull(value, "FieldName");
            action.Should().Throw<EntityValidationException>()
                .WithMessage("FieldName should not be null");

        }


        [Theory(DisplayName = "NotNullOrEmptyThowWhenEmpy")]
        [Trait("Domain", "DomainValidation - Validation")]
        [InlineData("")]
        [InlineData("  ")]
        [InlineData(null)]
        public void NotNullOrEmptyThowWhenEmpy(string? target)
        {
            string? value = null;
            Action action = () => DomainValidation.NotBeNullOrEmpty(target, "FieldName");
            action.Should().Throw<EntityValidationException>()
                .WithMessage("FieldName should not be empty or null");

        }

        [Fact(DisplayName = "NotNullOrEmptyOk")]
        [Trait("Domain", "DomainValidation - Validation")]
        public void NotNullOrEmptyOk()
        {
            var value = Faker.Commerce.ProductName();

            Action action = () => DomainValidation.NotBeNullOrEmpty(value, "value");

            action.Should().NotThrow();

        }

        [Theory(DisplayName = nameof(MinLengthThrowWhenLess))]
        [Trait("Domain", "DomainValidation - Validation")]
        [MemberData(nameof(GetValuesSmallerThanMin), parameters: 10)]
        public void MinLengthThrowWhenLess(string target, int minLength)
        {
            string fieldName = Faker.Commerce.ProductName().Replace(" ", "");

            Action action =
                () => DomainValidation.MinLength(target, minLength, fieldName);

            action.Should().Throw<EntityValidationException>()
                .WithMessage($"{fieldName} should be at least {minLength} characters long");
        }

        public static IEnumerable<object[]> GetValuesSmallerThanMin(int numberOfTests = 5)
        {
            var faker = new Faker();

            for (int i = 0; i < numberOfTests; i++)
            {
                var name = faker.Commerce.ProductName();
                var minLength = name.Length + (new Random()).Next(1, 20);

                yield return new object[] { name, minLength };
            }
        }


        [Theory(DisplayName = nameof(MinLengthOK))]
        [Trait("Domain", "DomainValidation - Validation")]
        [MemberData(nameof(GetValuesGreaterThanMin), parameters: 10)]
        public void MinLengthOK(string target, int minLength)
        {
            string fieldName = Faker.Commerce.ProductName().Replace(" ", "");

            Action action =
                () => DomainValidation.MinLength(target, minLength, fieldName);

            action.Should().NotThrow();
        }


        public static IEnumerable<object[]> GetValuesGreaterThanMin(int numberOfTests = 5)
        {
            var faker = new Faker();

            for (int i = 0; i < numberOfTests; i++)
            {
                var name = faker.Commerce.ProductName();
                var minLength = name.Length - 1;

                yield return new object[] { name, minLength };
            }
        }

        [Theory(DisplayName = nameof(MaxLengthThrowWhenGreater))]
        [Trait("Domain", "DomainValidation - Validation")]
        [MemberData(nameof(GetValuesGreaterThanMax), parameters: 10)]
        public void MaxLengthThrowWhenGreater(string target, int maxLength)
        {
            string fieldName = Faker.Commerce.ProductName().Replace(" ", "");

            Action action =
                () => DomainValidation.MaxLength(target, maxLength, fieldName);

            action.Should().Throw<EntityValidationException>()
                .WithMessage($"{fieldName} should be less or equal {maxLength} characters long");
        }

        public static IEnumerable<object[]> GetValuesGreaterThanMax(int numberOftests = 5)
        {
            yield return new object[] { "123456", 5 };
            var faker = new Faker();
            for (int i = 0; i < (numberOftests - 1); i++)
            {
                var example = faker.Commerce.ProductName();
                var maxLength = example.Length - (new Random()).Next(1, 5);
                yield return new object[] { example, maxLength };
            }
        }

        [Theory(DisplayName = nameof(MaxLengthOk))]
        [Trait("Domain", "DomainValidation - Validation")]
        [MemberData(nameof(GetValuesLessThanMax), parameters: 10)]
        public void MaxLengthOk(string target, int maxLength)
        {
            string fieldName = Faker.Commerce.ProductName().Replace(" ", "");

            Action action =
                () => DomainValidation.MaxLength(target, maxLength, fieldName);

            action.Should().NotThrow();
        }

        public static IEnumerable<object[]> GetValuesLessThanMax(int numberOftests = 5)
        {
            yield return new object[] { "123456", 6 };
            var faker = new Faker();
            for (int i = 0; i < (numberOftests - 1); i++)
            {
                var example = faker.Commerce.ProductName();
                var maxLength = example.Length + (new Random()).Next(0, 5);
                yield return new object[] { example, maxLength };
            }
        }
    }
}
