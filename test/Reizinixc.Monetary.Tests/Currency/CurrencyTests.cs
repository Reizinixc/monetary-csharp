namespace Reizinixc.Monetary.Tests.Currency
{
    using System;

    using Moq;

    using NUnit.Framework;

    using Reizinixc.Monetary.Currency;
    using Reizinixc.Monetary.Rounding;

    [TestFixture]
    [Parallelizable]
    public sealed partial class CurrencyTests
    {
        private static readonly string[] AlphabeticCodes = { "USD", "JPY" };

        private static readonly string[] EnglishNames = { "United States Dollar", "Japanese Yen" };

        private static readonly int[] InvalidDecimals = { -1, 29 };

        private static readonly uint[] NumericCodes = { 840u, 392u };

        private static readonly string[] NullOrWhitespaces =
        {
            null,
            string.Empty,
            " "
        };

        private static readonly IRoundingType[] RoundingTypes =
        {
            Mock.Of<IRoundingType>(),
            RoundingType.AwayFromZero
        };

        private static readonly string[] Symbols = { "$", "¥" };

        private static readonly int[] ValidDecimals = { 0, 28 };

        public sealed class Constructor
        {
            private static readonly ICurrency[] ConstructorCopyTestCases =
            {
                Mock.Of<ICurrency>(it =>
                    it.AlphabeticCode == "THD"
                    && it.Decimals == 2
                    && it.EnglishName == "Dollar"
                    && it.NumericCode == 999999u
                    && it.RoundingType == Mock.Of<IRoundingType>()
                    && it.Symbol == "T$"),
                new Currency("USD", 2, "Dollar", 840, RoundingType.AwayFromZero, "$")
            };

            [Test]
            [TestCaseSource(typeof(CurrencyTests.Constructor), nameof(ConstructorCopyTestCases))]
            public void CopyAllPropertiesForCurrencyInstance(ICurrency expectedCurrency)
            {
                // Arrange + Act
                var currency = new Currency(expectedCurrency);

                // Assert
                Assert.That(currency.AlphabeticCode, Is.EqualTo(expectedCurrency.AlphabeticCode));
                Assert.That(currency.Decimals, Is.EqualTo(expectedCurrency.Decimals));
                Assert.That(currency.EnglishName, Is.EqualTo(expectedCurrency.EnglishName));
                Assert.That(currency.NumericCode, Is.EqualTo(expectedCurrency.NumericCode));
                Assert.That(currency.RoundingType, Is.InstanceOf(expectedCurrency.RoundingType.GetType()));
                Assert.That(currency.Symbol, Is.EqualTo(expectedCurrency.Symbol));
            }

            [Test]
            [Pairwise]
            public void ReturnPassedArgumentForProvidedArguments(
                [ValueSource(typeof(CurrencyTests), nameof(AlphabeticCodes))] string alphabeticCode,
                [ValueSource(typeof(CurrencyTests), nameof(ValidDecimals))] int decimals,
                [ValueSource(typeof(CurrencyTests), nameof(EnglishNames))] string englishName,
                [ValueSource(typeof(CurrencyTests), nameof(NumericCodes))] uint numericCode,
                [ValueSource(typeof(CurrencyTests), nameof(RoundingTypes))] IRoundingType roundingType,
                [ValueSource(typeof(CurrencyTests), nameof(Symbols))] string symbol)
            {
                // Arrange + Act
                var currency = new Currency(alphabeticCode, decimals, englishName, numericCode, roundingType, symbol);

                // Assert
                Assert.That(currency.AlphabeticCode, Is.EqualTo(alphabeticCode));
                Assert.That(currency.EnglishName, Is.EqualTo(englishName));
                Assert.That(currency.NumericCode, Is.EqualTo(numericCode));
                Assert.That(currency.RoundingType, Is.InstanceOf(roundingType.GetType()));
                Assert.That(currency.Symbol, Is.EqualTo(symbol));
            }

            [Test]
            public void ThrowArgumentExceptionForNullOrWhiteSpaceAlphabeticCode(
                [ValueSource(typeof(CurrencyTests), nameof(NullOrWhitespaces))] string alphabeticCode)
            {
                // Act + Assert
                Assert.That(
                    () => new Currency(alphabeticCode, 2, "Test", 999999u, RoundingType.AwayFromZero, "T"),
                    Throws.ArgumentException);
            }

            [Test]
            public void ThrowArgumentOutOfRangeExceptionForInvalidDecimals(
                [ValueSource(typeof(CurrencyTests), nameof(InvalidDecimals))] int decimals)
            {
                // Act + Assert
                Assert.That(
                    () => new Currency("TTD", decimals, "Test", 999999u, RoundingType.AwayFromZero, "T"),
                    Throws.InstanceOf<ArgumentOutOfRangeException>());
            }

            [Test]
            public void ThrowArgumentExceptionForNullOrWhiteSpaceEnglishName(
                [ValueSource(typeof(CurrencyTests), nameof(NullOrWhitespaces))] string englishName)
            {
                // Act + Assert
                Assert.That(
                    () => new Currency("TTD", 2, englishName, 999999u, RoundingType.AwayFromZero, "T"),
                    Throws.ArgumentException);
            }

            [Test]
            public void ThrowArgumentNullExceptionForNullRoundingType()
            {
                // Act + Assert
                Assert.That(
                    () => new Currency("TTD", 2, "Test", 999999u, null, "T"),
                    Throws.ArgumentNullException);
            }

            [Test]
            public void ThrowArgumentExceptionForNullOrWhiteSpaceSymbol(
                [ValueSource(typeof(CurrencyTests), nameof(NullOrWhitespaces))] string symbol)
            {
                // Act + Assert
                Assert.That(
                    () => new Currency("TTD", 2, "Test", 999999u, RoundingType.AwayFromZero, symbol),
                    Throws.ArgumentException);
            }
        }

        public new sealed class Equals
        {
            [Test]
            public void ReturnFalseForNullInstance()
            {
                // Arrange
                var currency = new Currency("TTD", 2, "Test", 999999u, Mock.Of<IRoundingType>(), "T");

                // Act + Assert
                Assert.That(currency.Equals(null as object), Is.False);
                Assert.That(currency.Equals(null as ICurrency), Is.False);
            }

            [Test]
            public void ReturnFalseForDifferentInstanceType()
            {
                // Arrange
                var currency = new Currency("TTD", 2, "Test", 999999u, Mock.Of<IRoundingType>(), "T");

                // Act + Assert
                Assert.That(currency.Equals(new object()), Is.False);
            }

            [Test]
            [Pairwise]
            public void ReturnTrueForSameCurrencyValue(
                [ValueSource(typeof(CurrencyTests), nameof(AlphabeticCodes))] string alphabeticCode,
                [ValueSource(typeof(CurrencyTests), nameof(ValidDecimals))] int decimals,
                [ValueSource(typeof(CurrencyTests), nameof(EnglishNames))] string englishName,
                [ValueSource(typeof(CurrencyTests), nameof(NumericCodes))] uint numericCode,
                [ValueSource(typeof(CurrencyTests), nameof(Symbols))] string symbol)
            {
                // Arrange
                var currency = Mock.Of<ICurrency>(it =>
                    it.AlphabeticCode == alphabeticCode
                        && it.Decimals == decimals
                        && it.EnglishName == englishName
                        && it.NumericCode == numericCode
                        && it.RoundingType == RoundingType.AwayFromZero
                        && it.Symbol == symbol);
                var anotherCurrency = new Currency(currency);

                // Act + Assert
                Assert.That(anotherCurrency.Equals(currency as object), Is.True);
                Assert.That(anotherCurrency.Equals(currency as ICurrency), Is.True);
            }

            [Test]
            [Pairwise]
            public void ReturnTrueForTransitiveSameCurrencyValue(
                [ValueSource(typeof(CurrencyTests), nameof(AlphabeticCodes))] string alphabeticCode,
                [ValueSource(typeof(CurrencyTests), nameof(ValidDecimals))] int decimals,
                [ValueSource(typeof(CurrencyTests), nameof(EnglishNames))] string englishName,
                [ValueSource(typeof(CurrencyTests), nameof(NumericCodes))] uint numericCode,
                [ValueSource(typeof(CurrencyTests), nameof(Symbols))] string symbol)
            {
                // Arrange
                var currency = new Currency(alphabeticCode, decimals, englishName, numericCode, RoundingType.AwayFromZero, symbol);
                var currency1 = new Currency(alphabeticCode, decimals, englishName, numericCode, RoundingType.AwayFromZero, symbol);
                var anotherCurrency = new Currency(currency1);

                // Act + Assert
                Assert.That(currency.Equals(anotherCurrency as object), Is.True);
                Assert.That(currency.Equals(anotherCurrency as ICurrency), Is.True);
                Assert.That(currency1.Equals(anotherCurrency as object), Is.True);
                Assert.That(currency1.Equals(anotherCurrency as ICurrency), Is.True);
            }

            [Test]
            [Pairwise]
            public void ReturnTrueForSameInstance(
                [ValueSource(typeof(CurrencyTests), nameof(AlphabeticCodes))] string alphabeticCode,
                [ValueSource(typeof(CurrencyTests), nameof(ValidDecimals))] int decimals,
                [ValueSource(typeof(CurrencyTests), nameof(EnglishNames))] string englishName,
                [ValueSource(typeof(CurrencyTests), nameof(NumericCodes))] uint numericCode,
                [ValueSource(typeof(CurrencyTests), nameof(RoundingTypes))] IRoundingType roundingType,
                [ValueSource(typeof(CurrencyTests), nameof(Symbols))] string symbol)
            {
                // Arrange
                var currency = new Currency(alphabeticCode, decimals, englishName, numericCode, roundingType, symbol);

                // Act + Assert
                Assert.That(currency.Equals(currency as object), Is.True);
                Assert.That(currency.Equals(currency as ICurrency), Is.True);
            }
        }

        public sealed class GetHashCode
        {
            [Test]
            [Pairwise]
            public void ReturnSameHashCodeForSameCurrencyValue(
                [ValueSource(typeof(CurrencyTests), nameof(AlphabeticCodes))] string alphabeticCode,
                [ValueSource(typeof(CurrencyTests), nameof(ValidDecimals))] int decimals,
                [ValueSource(typeof(CurrencyTests), nameof(EnglishNames))] string englishName,
                [ValueSource(typeof(CurrencyTests), nameof(NumericCodes))] uint numericCode,
                [ValueSource(typeof(CurrencyTests), nameof(RoundingTypes))] IRoundingType roundingType,
                [ValueSource(typeof(CurrencyTests), nameof(Symbols))] string symbol)
            {
                // Arrange
                var currency = new Currency(alphabeticCode, decimals, englishName, numericCode, roundingType, symbol);
                var anotherCurrency = new Currency(alphabeticCode, decimals, englishName, numericCode, roundingType, symbol);

                // Act + Assert
                Assert.That(currency.GetHashCode(), Is.EqualTo(anotherCurrency.GetHashCode()));
            }
        }

        public sealed class Round
        {
            [Test]
            public void ReturnRoundedValueFromRoundingTypeInstance(
                [Values(0, 50)] decimal value,
                [ValueSource(typeof(CurrencyTests), nameof(ValidDecimals))] int decimals)
            {
                // Arrange
                var rounder = new Mock<IRoundingType>();
                var currency = new Currency("USD", decimals, "United States Dollar", 840u, rounder.Object, "$");

                // Act
                currency.Round(value);

                // Assert
                rounder.Verify(it => it.Round(value, decimals), Times.AtLeastOnce);
            }
        }
    }
}
