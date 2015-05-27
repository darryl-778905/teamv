using System;
using System.Runtime.Serialization;

namespace MobilePoll.Application
{
    [Serializable]
    public class InvalidSurveyQuestionTypeException : Exception
    {
        //
        // For guidelines regarding the creation of new exception types, see
        //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/cpgenref/html/cpconerrorraisinghandlingguidelines.asp
        // and
        //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/dncscol/html/csharp07192001.asp
        //

        public InvalidSurveyQuestionTypeException()
        {
        }

        public InvalidSurveyQuestionTypeException(string message) : base(message)
        {
        }

        public InvalidSurveyQuestionTypeException(string message, Exception inner) : base(message, inner)
        {
        }

        protected InvalidSurveyQuestionTypeException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
    }
}