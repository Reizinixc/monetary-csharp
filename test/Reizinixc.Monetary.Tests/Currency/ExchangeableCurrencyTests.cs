namespace Reizinixc.Monetary.Tests.Currency
{
    using System;
    using System.Collections;

    using Moq;

    using NUnit.Framework;

    using Reizinixc.Monetary.Currency;

    [TestFixture]
    [Parallelizable]
    public sealed class ExchangeableCurrencyTests
    {
        private static readonly decimal[] ValidExchangeRates =
        {
            0.01m,
            1m
        };

        [Test]
        public void ReturnCurrencyInfoFromProvidedCurrency()
        {
            // Arrange
            var currencyInfo = new Mock<ICurrency>();
            var currency = new ExchangeableCurrency(currencyInfo.Object, 0m);

            // Act
            var alphabeticCode = currency.AlphabeticCode;
            var decimals = currency.Decimals;
            var englishName = currency.EnglishName;
            var numericCode = currency.NumericCode;
            var roundingType = currency.RoundingType;
            var symbol = currency.Symbol;

            // Assert
            currencyInfo.VerifyGet(it => it.AlphabeticCode, Times.Once);
            currencyInfo.VerifyGet(it => it.Decimals, Times.Once);
            currencyInfo.VerifyGet(it => it.EnglishName, Times.Once);
            currencyInfo.VerifyGet(it => it.NumericCode, Times.Once);
            currencyInfo.VerifyGet(it => it.RoundingType, Times.Once);
            currencyInfo.VerifyGet(it => it.Symbol, Times.Once);
        }

        public sealed class Constructor
        {
            private static readonly ICurrency[] ValidCurrencies =
            {
                Mock.Of<ICurrency>(),
                Mock.Of<IExchangeableCurrency>()
            };

            [Test]
            [Pairwise]
            public void ReturnPassedArgumentForProvidedArguments(
                [ValueSource(typeof(ExchangeableCurrencyTests.Constructor), nameof(ValidCurrencies))] ICurrency currencyInfo,
                [ValueSource(typeof(ExchangeableCurrencyTests), nameof(ValidExchangeRates))] decimal exchangeRate)
            {
                // Arrange + Act
                var currency = new ExchangeableCurrency(currencyInfo, exchangeRate);

                // Assert
                Assert.That(currency.ExchangeRate, Is.EqualTo(exchangeRate));
            }

            [Test]
            [Pairwise]
            public void CreateSuccessfullyForValidArguments(
                [ValueSource(typeof(ExchangeableCurrencyTests.Constructor), nameof(ValidCurrencies))] ICurrency currency,
                [ValueSource(typeof(ExchangeableCurrencyTests), nameof(ValidExchangeRates))] decimal exchangeRate)
            {
                // Act + Assert
                Assert.That(() => new ExchangeableCurrency(currency, exchangeRate), Throws.Nothing);
            }

            [Test]
            public void ThrowArgumentNullExceptionForNullCurrency()
            {
                // Act + Assert
                Assert.That(() => new ExchangeableCurrency(null, 1m), Throws.ArgumentNullException);
            }

            [Test]
            public void ThrowArgumentOutOfRangeExceptionForNegativeExchangeRate()
            {
                // Act + Assert
                Assert.That(
                    () => new ExchangeableCurrency(Mock.Of<ICurrency>(), -0.01m),
                    Throws.InstanceOf<ArgumentOutOfRangeException>());
            }
        }

        public new sealed class Equals
        {
            [Test]
            public void ReturnResultFromICurrencyForICurrencyInstance()
            {
                // Arrange
                var currencyInfo = new Mock<ICurrency>();
                var currency1 = new ExchangeableCurrency(currencyInfo.Object, 0m);
                var currency2 = currencyInfo.Object;

                // Act
                currency1.Equals(currency2);

                // Assert
                currencyInfo.Verify(it => it.Equals(currency2), Times.Once);
            }

            [Test]
            public void ReturnFalseForNullInstance()
            {
                // Arrange
                var currency = new ExchangeableCurrency(Mock.Of<ICurrency>(), 0m);

                // Act + Assert
                Assert.That(currency.Equals(null), Is.False);
            }

            [Test]
            public void ReturnTrueForSameInstance()
            {
                // Arrange
                var currency = new ExchangeableCurrency(Mock.Of<ICurrency>(), 0m);

                // Act + Assert
                Assert.That(currency.Equals(currency), Is.True);
            }

            [Test]
            public void ReturnTrueForSameExchangeRate(
                [ValueSource(typeof(ExchangeableCurrencyTests), nameof(ValidExchangeRates))] decimal exchangeRate)
            {
                // Arrange
                var currency1 = new ExchangeableCurrency(Mock.Of<ICurrency>(), exchangeRate);
                var currency2 = new ExchangeableCurrency(Mock.Of<ICurrency>(), exchangeRate);

                // Act + Assert
                Assert.That(currency1.Equals(currency2), Is.True);
                Assert.That(currency2.Equals(currency1), Is.True);
            }

            [Test]
            public void ReturnFalseForDifferentExchangeRate(
                [ValueSource(typeof(ExchangeableCurrencyTests), nameof(ValidExchangeRates))] decimal exchangeRate)
            {
                // Arrange
                var currency1 = new ExchangeableCurrency(Mock.Of<ICurrency>(), exchangeRate);
                var currency2 = new ExchangeableCurrency(Mock.Of<ICurrency>(), exchangeRate + 1);

                // Act + Assert
                Assert.That(currency1.Equals(currency2), Is.False);
                Assert.That(currency2.Equals(currency1), Is.False);
            }
        }

        public sealed class Exchange
        {
            public static readonly IEnumerable TestCases = new[]
            {
                new TestCaseData(1m, 1m, 1m).Returns(1m),
                new TestCaseData(10m, 1m, 101.74637m).Returns(1017.4637m),
                new TestCaseData(2.9241m, 101.74637m, 34.79967m).Returns(2.9241m / 101.74637m * 34.79967m)
            };

            public static readonly object[][] UnexchangeableCases =
            {
                new object[] { 1m, 0m },
                new object[] { 0m, 1m }
            };

            [Test]
            [TestCaseSource(typeof(ExchangeableCurrencyTests.Exchange), nameof(TestCases))]
            public decimal CalculateCorrectlyForProvidedValues(decimal value, decimal valueExchangeRate, decimal targetExchangeRate)
            {
                // Arrange
                var valueCurrency = new ExchangeableCurrency(Mock.Of<ICurrency>(), valueExchangeRate);
                var targetCurrency = new ExchangeableCurrency(Mock.Of<ICurrency>(), targetExchangeRate);

                // Act + Assert
                return valueCurrency.Exchange(value, targetCurrency);
            }

            [Test]
            public void ThrowArgumentNullExceptionForNullTargetCurrency()
            {
                // Arrange
                var currency = new ExchangeableCurrency(Mock.Of<ICurrency>(), 1m);

                // Act + Assert
                Assert.That(() => currency.Exchange(1m, null), Throws.ArgumentNullException);
            }

            [Test]
            public void ThrowInvalidOperationExceptionForUnexchangeableCurrency()
            {
                // Arrange
                var valueCurrency = new ExchangeableCurrency(Mock.Of<ICurrency>(), 0m);
                var targetCurrency = new ExchangeableCurrency(Mock.Of<ICurrency>(), 1m);

                // Act + Assert
                Assert.That(() => valueCurrency.Exchange(1m, targetCurrency), Throws.InstanceOf<InvalidOperationException>());
            }

            [Test]
            [TestCaseSource(typeof(ExchangeableCurrencyTests.Exchange), nameof(UnexchangeableCases))]
            public void ThrowArgumentExceptionForUnexchangeableTargetCurrency(decimal valueExchangeRate, decimal targetExchangeRate)
            {
                // Arrange
                var valueCurrency = new ExchangeableCurrency(Mock.Of<ICurrency>(), 1m);
                var targetCurrency = new ExchangeableCurrency(Mock.Of<ICurrency>(), 0m);

                // Act + Assert
                Assert.That(() => valueCurrency.Exchange(1m, targetCurrency), Throws.ArgumentException);
            }
        }

        public sealed class Round
        {
            [Test]
            public void ReturnValueFromPassedCurrency(
                [ValueSource(typeof(ExchangeableCurrencyTests), nameof(ValidExchangeRates))] decimal value)
            {
                // Arrange
                var currencyInfo = new Mock<ICurrency>();
                var currency = new ExchangeableCurrency(currencyInfo.Object, 1m);

                // Act
                currency.Round(value);

                // Assert
                currencyInfo.Verify(it => it.Round(value), Times.Once);
            }
        }
    }
}
