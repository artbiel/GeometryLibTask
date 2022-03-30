using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometryLib.Exceptions
{
    public class GeometryLibException : Exception
    {
        public GeometryLibException(string message) : base(message) { }
    }
}