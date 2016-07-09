namespace Reizinixc.Monetary.Currency
{
    using System;

    using Reizinixc.Monetary.Rounding;

    /// <summary>
    /// Represents an accepted form of monetary that able to exchange to other currencies.
    /// </summary>
    public class ExchangeableCurrency : IExchangeableCurrency
    {
        /// <summary>
        /// An instance of currency information.
        /// </summary>
        private readonly ICurrency currency;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExchangeableCurrency" /> class with currency information and
        /// exchange rate of the currency.
        /// </summary>
        /// <param name="currency">An instance of currency information.</param>
        /// <param name="exchangeRate">An exchange rate value to be used in calculation.</param>
        /// <exception cref="ArgumentNullException"><paramref name="currency" /> is <see langword="null" />.</exception>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="exchangeRate" /> is negative value.</exception>
        public ExchangeableCurrency(ICurrency currency, decimal exchangeRate)
        {
            if (currency == null)
            {
                throw new ArgumentNullException(nameof(currency));
            }

            if (exchangeRate < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(exchangeRate), "Exchange rate of currency cannot be negative value.");
            }

            this.currency = currency;
            this.ExchangeRate = exchangeRate;
        }

        /// <inheritdoc />
        public string AlphabeticCode => this.currency.AlphabeticCode;

        /// <inheritdoc />
        public int Decimals => this.currency.Decimals;

        /// <inheritdoc />
        public string EnglishName => this.currency.EnglishName;

        /// <summary>
        /// Gets an exchange rate value.
        /// </summary>
        public decimal ExchangeRate { get; }

        /// <inheritdoc />
        public uint NumericCode => this.currency.NumericCode;

        /// <inheritdoc />
        public IRoundingType RoundingType => this.currency.RoundingType;

        /// <inheritdoc />
        public string Symbol => this.currency.Symbol;

        /// <inheritdoc />
        public bool Equals(ICurrency other)
        {
            return this.currency.Equals(other);
        }

        /// <inheritdoc />
        public bool Equals(IExchangeableCurrency other)
        {
            if (object.ReferenceEquals(null, other))
            {
                return false;
            }

            if (object.ReferenceEquals(this, other))
            {
                return true;
            }

            return this.ExchangeRate == other.ExchangeRate;
        }

        /// <inheritdoc />
        /// <exception cref="ArgumentNullException"><paramref name="target" /> is <see langword="null" />.</exception>
        /// <exception cref="ArgumentException"><paramref name="target" /> cannot do exchange.</exception>
        /// <exception cref="InvalidOperationException"><see langword="this" /> cannot do exchange.</exception>
        public decimal Exchange(decimal value, IExchangeable<decimal, decimal, decimal> target)
        {
            if (this.ExchangeRate <= 0)
            {
                throw new InvalidOperationException("The currency cannot do exchange.");
            }

            if (target == null)
            {
                throw new ArgumentNullException(nameof(target));
            }

            if (target.ExchangeRate <= 0)
            {
                throw new ArgumentException("The target currency cannot do exchange.");
            }

            return value / this.ExchangeRate * target.ExchangeRate;
        }

        /// <inheritdoc />
        public decimal Round(decimal value)
        {
            return this.currency.Round(value);
        }
    }
}
