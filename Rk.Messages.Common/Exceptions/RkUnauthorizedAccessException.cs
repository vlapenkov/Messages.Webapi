using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rk.Messages.Common.Exceptions
{
    public class RkUnauthorizedAccessException : RkErrorException
    {
        public RkUnauthorizedAccessException(string message) : base(message)
        { }
    }
}
