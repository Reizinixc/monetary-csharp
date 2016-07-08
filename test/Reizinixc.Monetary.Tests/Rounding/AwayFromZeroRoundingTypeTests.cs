namespace Reizinixc.Monetary.Tests.Rounding
{
    using System;
    using System.Collections;

    using Moq;

    using NUnit.Framework;

    using Reizinixc.Monetary.Rounding;

    [TestFixture]
    [Parallelizable]
    public sealed class AwayFromZeroRoundingTypeTests
    {
        private static readonly IEnumerable TestCases = new[]
        {
            new TestCaseData(0.00m, 0).Returns(0),
            new TestCaseData(0.49m, 0).Returns(0),
            new TestCaseData(0.50m, 0).Returns(1),
            new TestCaseData(0.99m, 0).Returns(1),
            new TestCaseData(1m, 0).Returns(1),
            new TestCaseData(1.01m, 0).Returns(1),
            new TestCaseData(-0.00m, 0).Returns(0),
            new TestCaseData(-0.49m, 0).Returns(0),
            new TestCaseData(-0.50m, 0).Returns(-1),
            new TestCaseData(-0.99m, 0).Returns(-1),
            new TestCaseData(-1m, 0).Returns(-1),
            new TestCaseData(-1.01m, 0).Returns(-1)
        };

        public new sealed class Equals
        {
            [Test]
            public void ReturnFalseForNullInstance()
            {
                // Arrange
                var rounder = new AwayFromZeroRoundingType();

                // Act + Assert
                Assert.That(rounder.Equals(null as object), Is.False);
                Assert.That(rounder.Equals(null as IRoundingType), Is.False);
            }

            [Test]
            public void ReturnFalseForDifferentInstanceType()
            {
                // Arrange
                var rounder = new AwayFromZeroRoundingType();
                var anotherRounder = Mock.Of<IRoundingType>(
                    it => it.GetHashCode() == nameof(AwayFromZeroRoundingType).GetHashCode());

                // Act + Assert
                Assert.That(rounder.Equals(anotherRounder as object), Is.False);
                Assert.That(rounder.Equals(anotherRounder as IRoundingType), Is.False);
            }

            [Test]
            public void ReturnTrueForSameInstance()
            {
                // Arrange
                var rounder = new AwayFromZeroRoundingType();

                // Act + Assert
                Assert.That(rounder.Equals(rounder as object), Is.True);
                Assert.That(rounder.Equals(rounder as IRoundingType), Is.True);
            }

            [Test]
            public void ReturnTrueForSameInstanceType()
            {
                // Arrange
                var rounder = new AwayFromZeroRoundingType();
                var anotherRounder = new AwayFromZeroRoundingType();

                // Act + Assert
                Assert.That(rounder.Equals(anotherRounder as object), Is.True);
                Assert.That(rounder.Equals(anotherRounder as IRoundingType), Is.True);
            }
        }

        public new sealed class GetHashCode
        {
            [Test]
            public void ReturnCorrectHashCodeNumber()
            {
                // Arrange
                var expectedHashCode = nameof(AwayFromZeroRoundingType).GetHashCode();
                var rounder = new AwayFromZeroRoundingType();

                // Act + Assert
                Assert.That(rounder.GetHashCode(), Is.EqualTo(expectedHashCode));
            }
        }

        public sealed class Round
        {
            [Test]
            [TestCaseSource(typeof(AwayFromZeroRoundingTypeTests), nameof(TestCases))]
            public decimal RoundCorrectlyForProvidedValues(decimal value, int decimals)
            {
                // Arrange
                var rounder = new AwayFromZeroRoundingType();

                // Act + Assert
                return rounder.Round(value, decimals);
            }

            [Test]
            [TestCase(-1)]
            [TestCase(29)]
            public void ThrowArgumentOutOfRangeExceptionForInvalidDecimals(int decimals)
            {
                // Arrange
                var rounder = new AwayFromZeroRoundingType();

                // Act + Assert
                Assert.That(
                    () => rounder.Round(12.23m, decimals),
                    Throws.InstanceOf<ArgumentOutOfRangeException>());
            }
        }
    }
}
