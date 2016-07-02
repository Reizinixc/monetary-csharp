namespace Reizinixc.Monetary.Currency
{
    using System;

    using Reizinixc.Monetary.Rounding;

    /// <summary>
    /// Represents an accepted form of monetary.
    /// </summary>
    public partial class Currency
    {
        /// <summary>
        /// Represents an <see cref="ICurrency" /> builder that separates the instance construction to
        /// step-by-step construction.
        /// </summary>
        public class Builder
        {
            /// <summary>
            /// An instance of currency data.
            /// </summary>
            private readonly Currency.Data data = new Currency.Data();

            /// <summary>
            /// Builds a new instance of <see cref="ICurrency" /> using provided information from builder.
            /// </summary>
            /// <returns>An instance of <see cref="ICurrency" /> contains set data from builder.</returns>
            public ICurrency Build()
            {
                return new Currency(this.data);
            }

            /// <summary>
            /// Sets a alphabetic code with provided argument.
            /// </summary>
            /// <param name="alphabeticCode">An alphabetic code of currency defines in ISO 4217.</param>
            /// <returns>The invoker of builder instance.</returns>
            /// <exception cref="ArgumentException">
            /// <paramref name="alphabeticCode" /> is <see langword="null" /> or contains only whitespaces.
            /// </exception>
            public Currency.Builder WithAlphabeticCode(string alphabeticCode)
            {
                this.data.AlphabeticCode = alphabeticCode;

                return this;
            }

            /// <summary>
            /// Sets a number of decimals with provided argument.
            /// </summary>
            /// <param name="decimals">A number of currency decimal or exponent.</param>
            /// <returns>The invoker of builder instance.</returns>
            /// <exception cref="ArgumentOutOfRangeException">
            /// <paramref name="decimals" /> is negative or more than 28.
            /// </exception>
            public Currency.Builder WithDecimals(int decimals)
            {
                this.data.Decimals = decimals;

                return this;
            }

            /// <summary>
            /// Sets an English name with provided argument.
            /// </summary>
            /// <param name="englishName">An English name of currency.</param>
            /// <returns>The invoker of builder instance.</returns>
            /// <exception cref="ArgumentException">
            /// <paramref name="englishName" /> is <see langword="null" /> or contains only whitespaces.
            /// </exception>
            public Currency.Builder WithEnglishName(string englishName)
            {
                this.data.EnglishName = englishName;

                return this;
            }

            /// <summary>
            /// Sets a numeric code with provided argument.
            /// </summary>
            /// <param name="numericCode">A numeric code of currency defines in ISO 4217.</param>
            /// <returns>The invoker of builder instance.</returns>
            public Currency.Builder WithNumericCode(uint numericCode)
            {
                this.data.NumericCode = numericCode;

                return this;
            }

            /// <summary>
            /// Sets a rounding type with provided argument.
            /// </summary>
            /// <param name="roundingType">Type of rounding of currency.</param>
            /// <returns>The invoker of builder instance.</returns>
            /// <exception cref="ArgumentNullException">
            /// <paramref name="roundingType" /> is <see langword="null" />.
            /// </exception>
            public Currency.Builder WithRoundingType(IRoundingType roundingType)
            {
                this.data.RoundingType = roundingType;

                return this;
            }

            /// <summary>
            /// Sets a symbol with provided argument.
            /// </summary>
            /// <param name="symbol">A symbol that indicate the currency.</param>
            /// <returns>The invoker of builder instance.</returns>
            /// <exception cref="ArgumentException">
            /// <paramref name="symbol" /> is <see langword="null" /> or contains only whitespaces.
            /// </exception>
            public Currency.Builder WithSymbol(string symbol)
            {
                this.data.Symbol = symbol;

                return this;
            }
        }
    }
}
