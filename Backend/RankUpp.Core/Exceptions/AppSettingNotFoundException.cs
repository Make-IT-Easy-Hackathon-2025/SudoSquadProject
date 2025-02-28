using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace RankUpp.Core.Exceptions
{
    public class AppSettingNotFoundException : Exception
    {
        public AppSettingNotFoundException()
        {
        }

        public AppSettingNotFoundException(string? message) : base(message)
        {
        }

        public AppSettingNotFoundException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
