using Bogus;
using Codeflix.Catalog.Application.Interfaces;
using Codeflix.Catalog.Domain.Repository;
using Moq;

namespace Codeflix.Catalog.UnitTests.Common
{
    public abstract class BaseFixture
    {
        public Faker Faker { get; set; }

        protected BaseFixture()
            => Faker = new Faker("pt_BR");

        public bool GetRandomBoolean()
            => new Random().NextDouble() < 0.5;

        public Mock<ICategoryRepository> GetCategoryRepositoryMock()
        {
            return new();
        }

        public Mock<IUnitOfWork> GetUnitOfWorkMock()
        {
            return new();
        }
    }
}
