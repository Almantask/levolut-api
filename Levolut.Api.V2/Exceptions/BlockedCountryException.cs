using System.Runtime.Serialization;

namespace Levolut.Api.V2.Exceptions
{
    [Serializable]
    public class BlockedCountryException : Exception
    {
        public string Country { get; set; }

        public BlockedCountryException()
        {
        }

        public BlockedCountryException(string country) : base($"Country {country} is blocked.")
        {
            Country = country;
        }

        public BlockedCountryException(string message, Exception inner) : base(message, inner)
        {
        }

        protected BlockedCountryException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
    }
}
