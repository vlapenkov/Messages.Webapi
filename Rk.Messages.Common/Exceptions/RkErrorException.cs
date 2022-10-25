using System;

namespace Rk.Messages.Common.Exceptions
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
