using GeometryLib.Models;
using FluentAssertions;
using System;
using Xunit;

namespace GeometryLib.Tests
{
    public class CircleTests
    {
        [Theory]
        [InlineData(1)]
        [InlineData(5.5)]
        [InlineData(100)]
        public void Constructor_ShouldCreateCircle_WhenParametersAreValid(double radius)
        {
            // Act
            var circle = new Circle(radius);

            // Assert
            circle.Should().NotBeNull();
            circle.Radius.Should().Be(radius);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-10)]
        public void Constructor_ShouldThrowArgumentOutOfRangeException_WhenRadiusIsZeroOrNegative(double radius)
        {
            // Act
            var act = () => _ = new Circle(radius);

            // Assert
            act.Should().Throw<ArgumentOutOfRangeException>();
        }

        [Fact]
        public void GetArea_ShouldReturnValidArea()
        {
            // Assert
            var circle = new Circle(3);
            var expectedArea = 9 * Math.PI;

            // Act
            var area = circle.GetArea();

            // Assert
            area.Should().Be(expectedArea);
        }

        [Theory]
        [MemberData(nameof(EqualCircles))]
        public void Equals_ShouldReturnTrue_WhenRadiusesAreEqual(Circle circle1, Circle circle2)
        {
            // Act
            var equals = circle1.Equals(circle2);

            // Assert
            equals.Should().BeTrue();
        }

        [Theory]
        [MemberData(nameof(NotEqualCircles))]
        public void Equals_ShouldReturnFalse_WhenRadiusesAreNotEqual(Circle circle1, Circle circle2)
        {
            // Act
            var equals = circle1.Equals(circle2);

            // Assert
            equals.Should().BeFalse();
        }

        public static TheoryData<Circle, Circle> EqualCircles => new()
        {
            {
                new Circle(1),
                new Circle(1)
            },
            {
                new Circle(5),
                new Circle(5)
            }
        };

        public static TheoryData<Circle, Circle> NotEqualCircles => new()
        {
            {
                new Circle(1),
                new Circle(2)
            },
            {
                new Circle(5),
                new Circle(10)
            }
        };
    }
}