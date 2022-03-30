using GeometryLib.Exceptions;
using GeometryLib.Models;
using FluentAssertions;
using System;
using Xunit;

namespace GeometryLib.Tests
{
    public class TriangleTests
    {
        [Theory]
        [InlineData(1, 1, 1.5)]
        [InlineData(3, 2, 3)]
        [InlineData(1.8, 1, 1)]
        [InlineData(5, 1, 5)]
        [InlineData(1, 1, 1)]
        public void Constructor_ShouldCreateTriangle_WhenParametersAreValid(double a, double b, double c)
        {
            // Act
            var triangle = new Triangle(a, b, c);

            // Assert
            triangle.Should().NotBeNull();
            triangle.A.Should().Be(a);
            triangle.B.Should().Be(b);
            triangle.C.Should().Be(c);
        }

        [Theory]
        [InlineData(0, 1, 1.5)]
        [InlineData(1, -0.01, 3)]
        [InlineData(0, 0, -10)]
        public void Constructor_ShouldThrowArgumentOutOfRangeException_WhenSidesAreZeroOrNegative(double a, double b, double c)
        {
            // Act
            var act = () => _ = new Triangle(a, b, c);

            // Assert
            act.Should().Throw<ArgumentOutOfRangeException>();
        }

        [Theory]
        [InlineData(1, 1, 5)]
        [InlineData(1, 2, 3)]
        [InlineData(5, 5, 10)]
        public void Constructor_ShouldThrowIncorrectTriangleSidesException_WhenTrangleDoesntExists(double a, double b, double c)
        {
            // Act
            var act = () => _ = new Triangle(a, b, c);

            // Assert
            act.Should().Throw<IncorrectTriangleSidesException>();
        }

        [Theory]
        [InlineData(3, 4, 5)]
        [InlineData(5, 12, 13)]
        [InlineData(7, 24, 25)]
        public void IsRight_ShouldReturnTrue_WhenTrangleIsRight(double a, double b, double c)
        {
            var triangle = new Triangle(a, b, c);

            // Act
            var isRight = triangle.IsRight;

            // Assert
            isRight.Should().BeTrue();
        }

        [Theory]
        [InlineData(5, 4, 5)]
        [InlineData(5, 11, 13)]
        [InlineData(7, 22, 25)]
        public void IsRight_ShouldReturnFalse_WhenTrangleIsNotRight(double a, double b, double c)
        {
            var triangle = new Triangle(a, b, c);

            // Act
            var isRight = triangle.IsRight;

            // Assert
            isRight.Should().BeFalse();
        }

        [Fact]
        public void GetArea_ShouldReturnValidArea()
        {
            // Assert
            var triangle = new Triangle(3, 4, 5);
            var expectedArea = 6.0;

            // Act
            var area = triangle.GetArea();

            // Assert
            area.Should().Be(expectedArea);
        }

        [Theory]
        [MemberData(nameof(EqualTriangles))]
        public void Equals_ShouldReturnTrue_WhenSidesAreEqual(Triangle triangle1, Triangle triangle2)
        {
            // Act
            var equals = triangle1.Equals(triangle2);

            // Assert
            equals.Should().BeTrue();
        }

        [Theory]
        [MemberData(nameof(NotEqualTriangles))]
        public void Equals_ShouldReturnFalse_WhenSidesAreNotEqual(Triangle triangle1, Triangle triangle2)
        {
            // Act
            var equals = triangle1.Equals(triangle2);

            // Assert
            equals.Should().BeFalse();
        }

        public static TheoryData<Triangle, Triangle> EqualTriangles => new()
        {
            {
                new Triangle(1, 1, 1.5),
                new Triangle(1, 1, 1.5)
            },
            {
                new Triangle(3, 2, 3),
                new Triangle(3, 3, 2)
            },
            {
                new Triangle(1.8, 1, 1),
                new Triangle(1, 1.8, 1)
            },
            {
                new Triangle(5, 1, 5),
                new Triangle(1, 5, 5)
            }
        };

        public static TheoryData<Triangle, Triangle> NotEqualTriangles => new()
        {
            {
                new Triangle(1, 1, 1.7),
                new Triangle(1, 1, 1.5)
            },
            {
                new Triangle(3, 3, 3),
                new Triangle(3, 3, 2)
            },
            {
                new Triangle(1.8, 1, 1),
                new Triangle(1, 2, 2.2)
            },
            {
                new Triangle(5, 1, 5),
                new Triangle(1, 4.3, 5)
            }
        };
    }
}