using GeometryLib.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace GeometryLib.Helpers
{
    internal static class Guard
    {
        internal static double AgainstNegativeOrZero(double value, [CallerArgumentExpression("value")] string paramName = "")
            => value > 0 ? value : throw new ArgumentOutOfRangeException(paramName, $"{paramName} should be positive number");

        internal static void AgainstIncorrectTriangleSides(double a, double b, double c)
        {
            if (a + b <= c || a + c <= b || b + c <= a)
                throw new IncorrectTriangleSidesException(a, b, c);
        }
    }
}