using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace RankUpp.Core.Exceptions
{
    public class WrongEmailOrPasswordException : Exception
    {
        public WrongEmailOrPasswordException()
        {
        }

        public WrongEmailOrPasswordException(string? message) : base(message)
        {
        }

        public WrongEmailOrPasswordException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected WrongEmailOrPasswordException(SerializationInfo info, StreamingContext context)
        {
        }
    }
}
