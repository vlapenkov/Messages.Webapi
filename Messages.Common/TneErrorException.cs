using System;
using System.Collections.Generic;
using System.Text;

namespace Messages.Common
{
    public class TneErrorException : Exception
    {
        public TneErrorException(string message) : base( message )
        { }
    }
}
