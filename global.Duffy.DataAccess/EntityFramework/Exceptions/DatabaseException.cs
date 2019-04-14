using System;
using System.Collections.Generic;
using System.Text;

namespace global.Duffy.DataAccess.EntityFramework.Exceptions
{
    public class DatabaseException : Exception
    {
        public DatabaseException(string message) : base(message)
        {
        }
    }
}
