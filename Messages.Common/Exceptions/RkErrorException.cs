using System;
using System.Collections.Generic;
using System.Text;

namespace Messages.Common.Exceptions
{
    /// <summary>
    /// Любое бизнес-исключение
    /// </summary>
    public class RkErrorException : Exception
    {
        public RkErrorException(string message) : base(message)
        { }
    }
}
