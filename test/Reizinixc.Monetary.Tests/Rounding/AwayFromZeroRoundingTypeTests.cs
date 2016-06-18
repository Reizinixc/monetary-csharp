namespace Reizinixc.Monetary.Tests.Rounding
{
    using System;
    using System.Collections;

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
