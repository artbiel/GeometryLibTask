using GeometryLib.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometryLib.Models
{
    public class Circle : Shape
    {
        public Circle(double radius)
        {
            Radius = Guard.AgainstNegativeOrZero(radius);
        }

        public double Radius { get; }

        public override double GetArea() => Math.PI * Math.Pow(Radius, 2);

        public override bool Equals(object? obj) => obj is Circle c && c.Radius == Radius;
        public override int GetHashCode() => Radius.GetHashCode();
    }
}