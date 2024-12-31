using System.Runtime.Serialization;

namespace InfnetEcommerceContext.Cart.API.Services
{
    [Serializable]
    internal class NotFoundProduct : Exception
    {
        public NotFoundProduct()
        {
        }

        public NotFoundProduct(string message) : base(message)
        {
        }

        public NotFoundProduct(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected NotFoundProduct(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}