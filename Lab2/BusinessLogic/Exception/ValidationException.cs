using System.Runtime.Serialization;

namespace ComputerStore.BusinessLogicLayer.Exception
{
    internal class ValidationException : System.Exception
    {
        public ValidationException()
        {
        }

        public ValidationException(string message) : base(message)
        {
        }

        public ValidationException(string message, System.Exception innerException) : base(message, innerException)
        {
        }

        protected ValidationException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
