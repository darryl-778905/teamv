using System;
using System.Runtime.Serialization;

namespace MobilePoll.Infrastructure.Ioc
{
    [Serializable]
    public class ComponentRegistrationException : Exception
    {
        public ComponentRegistrationException()
        {
        }

        public ComponentRegistrationException(string message)
            : base(message)
        {
        }

        public ComponentRegistrationException(string message, Exception inner)
            : base(message, inner)
        {
        }

        protected ComponentRegistrationException(
            SerializationInfo info,
            StreamingContext context)
            : base(info, context)
        {
        }
    }
}
