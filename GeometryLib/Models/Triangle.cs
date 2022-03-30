using GeometryLib.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometryLib.Models
{
    public class Triangle : Shape
    {
        public Triangle(double a, double b, double c)
        {
            A = Guard.AgainstNegativeOrZero(a);
            B = Guard.AgainstNegativeOrZero(b);
            C = Guard.AgainstNegativeOrZero(c);
            Guard.AgainstIncorrectTriangleSides(a, b, c);
        }

        public double A { get; }
        public double B { get; }
        public double C { get; }

        public bool IsRight
        {
            get
            {
                Span<double> sides = stackalloc double[3] { A, B, C };
                sides.Sort();
                return Math.Pow(sides[0], 2) + Math.Pow(sides[1], 2) == Math.Pow(sides[2], 2);
            }
        }

        public override double GetArea()
        {
            double p = (A + B + C) / 2;
            return Math.Sqrt(p * (p - A) * (p - B) * (p - C));
        }

        public override bool Equals(object? obj)
        {
            if (obj is not Triangle other)
                return false;
            Span<double> thisSides = stackalloc double[3] { A, B, C };
            Span<double> otherSides = stackalloc double[3] { other.A, other.B, other.C };
            thisSides.Sort();
            otherSides.Sort();
            return thisSides.SequenceEqual(otherSides);
        }

        public override int GetHashCode() => A.GetHashCode() ^ B.GetHashCode() ^ C.GetHashCode();
    }
}