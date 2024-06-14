namespace Codeflix.Catalog.Domain.Exeptions
{
    public class EntityValidationException : Exception
    {
        public EntityValidationException(string? message) : base(message)
        {
        }
    }
}
