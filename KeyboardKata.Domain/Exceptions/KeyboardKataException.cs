using System.Runtime.Serialization;

namespace KeyboardKata.Domain.Exceptions
{
    public class KeyboardKataException : Exception
    {
        public KeyboardKataException()
        {
        }

        public KeyboardKataException(string? message) : base(message)
        {
        }

        public KeyboardKataException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected KeyboardKataException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
