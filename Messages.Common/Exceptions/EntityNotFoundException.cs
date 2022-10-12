using System;
using System.Collections.Generic;
using System.Text;

namespace Messages.Common.Exceptions
{
    /// <summary>
    /// Исключение для не найденной сущности
    /// </summary>
    public class EntityNotFoundException : RkErrorException
    {
        public EntityNotFoundException(string message) : base(message)
        { }
    }
}
