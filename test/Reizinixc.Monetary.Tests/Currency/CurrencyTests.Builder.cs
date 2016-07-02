namespace Reizinixc.Monetary.Tests.Currency
{
    using System;

    using Moq;

    using NUnit.Framework;

    using Reizinixc.Monetary.Currency;
    using Reizinixc.Monetary.Rounding;

    public sealed partial class CurrencyTests
    {
        public sealed class Builder
        {
            public sealed class Build
            {
                [Test]
                public void ReturnDefaultValueOfCurrencyInfoForImmediatelyBuild()
                {
                    // Arrange
                    var builder = new Currency.Builder();

                    // Act
                    var currency = builder.Build();

                    // Assert
                    Assert.That(currency.AlphabeticCode, Is.EqualTo(string.Empty));
                    Assert.That(currency.Decimals, Is.EqualTo(0));
                    Assert.That(currency.EnglishName, Is.EqualTo(string.Empty));
                    Assert.That(currency.NumericCode, Is.EqualTo(0u));
                    Assert.That(currency.RoundingType, Is.InstanceOf<AwayFromZeroRoundingType>());
                    Assert.That(currency.Symbol, Is.EqualTo(string.Empty));
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
                    // Arrange
                    var builder = new Currency.Builder();

                    // Act
                    var currency = builder
                        .WithAlphabeticCode(alphabeticCode)
                        .WithDecimals(decimals)
                        .WithEnglishName(englishName)
                        .WithNumericCode(numericCode)
                        .WithRoundingType(roundingType)
                        .WithSymbol(symbol)
                        .Build();

                    // Assert
                    Assert.That(currency.AlphabeticCode, Is.EqualTo(alphabeticCode));
                    Assert.That(currency.EnglishName, Is.EqualTo(englishName));
                    Assert.That(currency.NumericCode, Is.EqualTo(numericCode));
                    Assert.That(currency.RoundingType, Is.InstanceOf(roundingType.GetType()));
                    Assert.That(currency.Symbol, Is.EqualTo(symbol));
                }
            }

            public sealed class WithAlphabeticCode
            {
                [Test]
                public void ReturnSelfInstanceForProvidedArgument()
                {
                    // Arrange
                    var builder = new Currency.Builder();

                    // Act + Assert
                    Assert.That(builder.WithAlphabeticCode("USD"), Is.SameAs(builder));
                }

                [Test]
                public void ThrowArgumentExceptionForNullOrWhitespaceArgument(
                    [ValueSource(typeof(CurrencyTests), nameof(NullOrWhitespaces))] string alphabeticCode)
                {
                    // Arrange
                    var builder = new Currency.Builder();

                    // Act + Assert
                    Assert.That(() => builder.WithAlphabeticCode(alphabeticCode), Throws.ArgumentException);
                }
            }

            public sealed class WithDecimals
            {
                [Test]
                public void ReturnSelfInstanceForValidArgument(
                    [ValueSource(typeof(CurrencyTests), nameof(ValidDecimals))] int decimals)
                {
                    // Arrange
                    var builder = new Currency.Builder();

                    // Act + Assert
                    Assert.That(builder.WithDecimals(decimals), Is.SameAs(builder));
                }

                [Test]
                public void ThrowArgumentOutOfRangeExceptionForInvalidArgument(
                    [ValueSource(typeof(CurrencyTests), nameof(InvalidDecimals))] int decimals)
                {
                    // Arrange
                    var builder = new Currency.Builder();

                    // Act + Assert
                    Assert.That(
                        () => builder.WithDecimals(decimals),
                        Throws.InstanceOf<ArgumentOutOfRangeException>());
                }
            }

            public sealed class WithEnglishName
            {
                [Test]
                public void ReturnSelfInstanceForProvidedArgument()
                {
                    // Arrange
                    var builder = new Currency.Builder();

                    // Act + Assert
                    Assert.That(builder.WithEnglishName("Test"), Is.SameAs(builder));
                }

                [Test]
                public void ThrowArgumentExceptionForNullOrWhitespaceArgument(
                    [ValueSource(typeof(CurrencyTests), nameof(NullOrWhitespaces))] string englishName)
                {
                    // Arrange
                    var builder = new Currency.Builder();

                    // Act + Assert
                    Assert.That(() => builder.WithEnglishName(englishName), Throws.ArgumentException);
                }
            }

            public sealed class WithNumericCode
            {
                [Test]
                public void ReturnSelfInstanceForProvidedArgument()
                {
                    // Arrange
                    var builder = new Currency.Builder();

                    // Act + Assert
                    Assert.That(builder.WithNumericCode(123u), Is.SameAs(builder));
                }
            }

            public sealed class WithRoundingType
            {
                [Test]
                public void ReturnSelfInstanceForNotNullArgument()
                {
                    // Arrange
                    var builder = new Currency.Builder();

                    // Act + Assert
                    Assert.That(builder.WithRoundingType(Mock.Of<IRoundingType>()), Is.SameAs(builder));
                }

                [Test]
                public void ThrowArgumentNullExceptionForNullArgument()
                {
                    // Arrange
                    var builder = new Currency.Builder();

                    // Act + Assert
                    Assert.That(() => builder.WithRoundingType(null), Throws.ArgumentNullException);
                }
            }

            public sealed class WithSymbol
            {
                [Test]
                public void ReturnSelfInstanceForProvidedArgument()
                {
                    // Arrange
                    var builder = new Currency.Builder();

                    // Act + Assert
                    Assert.That(builder.WithSymbol("$"), Is.SameAs(builder));
                }

                [Test]
                public void ThrowArgumentExceptionForNullOrWhitespaceArgument(
                    [ValueSource(typeof(CurrencyTests), nameof(NullOrWhitespaces))] string symbol)
                {
                    // Arrange
                    var builder = new Currency.Builder();

                    // Act + Assert
                    Assert.That(() => builder.WithSymbol(symbol), Throws.ArgumentException);
                }
            }
        }
    }
}
