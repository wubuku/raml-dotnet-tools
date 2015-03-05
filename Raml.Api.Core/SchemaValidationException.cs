using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RAML.Api.Core
{
    public class SchemaValidationException : Exception
    {
        public SchemaValidationException() { }

        public SchemaValidationException(string message) : base(message) { }

        public SchemaValidationException(string message, Exception innerException) : base(message, innerException) { }
    }
}
