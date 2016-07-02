namespace Reizinixc.Monetary.Currency
{
    using Reizinixc.Monetary.Rounding;

    /// <summary>
    /// Represents an accepted form of monetary.
    /// </summary>
    public partial class Currency : ICurrency
    {
        /// <summary>
        /// An instance contains an information of currency.
        /// </summary>
        private readonly Currency.Data data;

        /// <summary>
        /// Initializes a new instance of the <see cref="Currency" /> class with an required information of currency.
        /// </summary>
        /// <param name="alphabeticCode">An alphabetic code of currency defines in ISO 4217.</param>
        /// <param name="decimals">A number of currency decimal or exponent.</param>
        /// <param name="englishName">An English name of currency.</param>
        /// <param name="numericCode">A numeric code of currency defines in ISO 4217.</param>
        /// <param name="roundingType">Type of rounding of currency.</param>
        /// <param name="symbol">A symbol that indicate the currency.</param>
        public Currency(
            string alphabeticCode,
            int decimals,
            string englishName,
            uint numericCode,
            IRoundingType roundingType,
            string symbol)
            : this(
                new Currency.Data
                {
                    AlphabeticCode = alphabeticCode,
                    Decimals = decimals,
                    EnglishName = englishName,
                    NumericCode = numericCode,
                    RoundingType = roundingType,
                    Symbol = symbol
                })
        {
            // Do nothing.
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Currency" /> class by using data from provided
        /// currency instance.
        /// </summary>
        /// <param name="currency">An instance to be provided information to the new instance.</param>
        public Currency(ICurrency currency)
        {
            var c = currency as Currency;

            if (c != null)
            {
                this.data = c.data;
            }
            else
            {
                this.data = new Currency.Data
                {
                    AlphabeticCode = currency.AlphabeticCode,
                    Decimals = currency.Decimals,
                    EnglishName = currency.EnglishName,
                    NumericCode = currency.NumericCode,
                    RoundingType = currency.RoundingType,
                    Symbol = currency.Symbol
                };
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Currency" /> class with provided currency data.
        /// </summary>
        /// <param name="data">An instance to be provided information to the new instance.</param>
        private Currency(Currency.Data data)
        {
            this.data = data;
        }

        /// <inheritdoc />
        public string AlphabeticCode => this.data.AlphabeticCode;

        /// <inheritdoc />
        public int Decimals => this.data.Decimals;

        /// <inheritdoc />
        public string EnglishName => this.data.EnglishName;

        /// <inheritdoc />
        public uint NumericCode => this.data.NumericCode;

        /// <inheritdoc />
        public IRoundingType RoundingType => this.data.RoundingType;

        /// <inheritdoc />
        public string Symbol => this.data.Symbol;

        /// <inheritdoc />
        public decimal Round(decimal value)
        {
            return this.RoundingType.Round(value, this.Decimals);
        }
    }
}
