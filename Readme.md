# Test Task using Source Generators

Generates AreaUtils class which provides helpers to calculate area of geometry shapes defined in project. It allows to:
- Easely add new shapes and then automaticaly area helpers for new shapes will be generated
- Avoid heap allocation when only need to calculate area

## Generating alghorithm:
1. Find classes derived from base class Shape
2. Get all properties used in GetArea method
3. Get constructor which initialize all this properties
4. Get constructor parameters which wil be user as htlper method parameters
5. Get all statements from constructor (validation & assignment)
6. Get method body depending is it block or lambda
7. Build helper method using pattern:
```
public static double Get{*className*}Area({constructor parameters})
{
  { all statements from constructor }
  { GetArea method body }
}
```

## Generated file looks like this:

```csharp
using GeometryLib.Helpers;

namespace AreaLib.Utils
{
    public static class AreaUtils
    {
        public static double GetCircleArea(double radius)
        {
            var Radius = Guard.AgainstNegativeOrZero(radius); 
            return Math.PI * Math.Pow(Radius, 2);
        }
        public static double GetTriangleArea(double a, double b, double c)
        {
            var A = Guard.AgainstNegativeOrZero(a);
            var B = Guard.AgainstNegativeOrZero(b);
            var C = Guard.AgainstNegativeOrZero(c);
            Guard.AgainstIncorrectTriangleSides(a, b, c); 
            double p = (A + B + C) / 2;
            return Math.Sqrt(p * (p - A) * (p - B) * (p - C));
        }
    }
}
```