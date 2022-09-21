using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BookMyShow.Core.Exceptions
{
    [Serializable]
    public class DuplicateException : Exception
    {
         public DuplicateException() : base() { }

        public DuplicateException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) : base(info, context)
        {

        }
        public DuplicateException(string message) : base(message) 
        {

        }
        public DuplicateException(string message, Exception innerException) : base(message,innerException)
        {

        }
    }
}
