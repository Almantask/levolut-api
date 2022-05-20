using System.Runtime.Serialization;

namespace Levolut.Api.V2.Exceptions
{
    [Serializable]
    internal class EntityNotFoundException : Exception
    {
        public string Entity { get; }

        public EntityNotFoundException()
        {
        }

        public EntityNotFoundException(string entity) : base($"{entity} not found.")
        {
        }

        public EntityNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected EntityNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}