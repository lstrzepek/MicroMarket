using System;
using System.Runtime.Serialization;

namespace MicroCompany
{
    [Serializable]
    internal class MaximumCapacityExceeded : Exception
    {
        public MaximumCapacityExceeded()
        {
        }

        public MaximumCapacityExceeded(string message) : base(message)
        {
        }

        public MaximumCapacityExceeded(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected MaximumCapacityExceeded(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}