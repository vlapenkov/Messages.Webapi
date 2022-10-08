using System;
using System.Collections.Generic;
using System.Text;

namespace Messages.Common
{
    public class EntityNotFoundException : Exception
    {
        public EntityNotFoundException(string message) : base( message )
        { }
    }
}
