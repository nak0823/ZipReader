using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZipReader.Exceptions
{
    public class InvalidZipFileException : Exception
    {
        public InvalidZipFileException(string message) : base(message)
        { 
        }
    }
}
