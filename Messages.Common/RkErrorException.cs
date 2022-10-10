using System;
using System.Collections.Generic;
using System.Text;

namespace Messages.Common
{
    public class RkErrorException : Exception
    {
        public RkErrorException(string message) : base( message )
        { }
    }
}
