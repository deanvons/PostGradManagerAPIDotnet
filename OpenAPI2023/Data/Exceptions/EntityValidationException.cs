namespace OpenAPI2023.Data.Exceptions
{
    public class EntityValidationException : Exception
    {
        public EntityValidationException(string? message) 
            : base(message)
        {
        }
    }
}
