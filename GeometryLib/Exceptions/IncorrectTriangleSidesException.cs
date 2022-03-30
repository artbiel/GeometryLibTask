using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometryLib.Exceptions
{
    public class IncorrectTriangleSidesException : GeometryLibException
    {
        public IncorrectTriangleSidesException(double a, double b, double c) 
            : base($"Triangle with sides {a}, {b}, {c} doesn't exists") { }
    }
}