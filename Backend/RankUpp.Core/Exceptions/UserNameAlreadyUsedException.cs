using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace RankUpp.Core.Exceptions
{
    public class UserNameAlreadyUsedException : Exception
    {
        public UserNameAlreadyUsedException()
        {
        }

        public UserNameAlreadyUsedException(string? message) : base(message)
        {
        }

        public UserNameAlreadyUsedException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
