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
        /// Represents a data container of <see cref="Currency" />.
        /// </summary>
        private class Data
        {
            /// <summary>
            /// An alphabetic code of currency defines in ISO 4217.
            /// </summary>
            private string alphabeticCode = string.Empty;

            /// <summary>
            /// A number of currency decimal or exponent.
            /// </summary>
            private int decimals;

            /// <summary>
            /// An English name of currency.
            /// </summary>
            private string englishName = string.Empty;

            /// <summary>
            /// Type of rounding of currency.
            /// </summary>
            private IRoundingType roundingType = Rounding.RoundingType.AwayFromZero;

            /// <summary>
            /// A symbol that indicate the currency.
            /// </summary>
            private string symbol = string.Empty;

            /// <summary>
            /// Gets or sets an alphabetic code of currency defines in ISO 4217.
            /// </summary>
            /// <exception cref="ArgumentException" accessor="set">
            /// <see langword="value" /> is <see langword="null" /> or contains only whitespaces.
            /// </exception>
            public string AlphabeticCode
            {
                get
                {
                    return this.alphabeticCode;
                }

                set
                {
                    if (string.IsNullOrWhiteSpace(value))
                    {
                        throw new ArgumentException("Alphabetic code cannot be null or whitespace", nameof(value));
                    }

                    this.alphabeticCode = value;
                }
            }

            /// <summary>
            /// Gets or sets a number of currency decimal or exponent.
            /// </summary>
            /// <exception cref="ArgumentOutOfRangeException" accessor="set">
            /// <see langword="value" /> is negative or more than 28.
            /// </exception>
            public int Decimals
            {
                get
                {
                    return this.decimals;
                }

                set
                {
                    if (value < 0)
                    {
                        throw new ArgumentOutOfRangeException(nameof(value), "Decimals cannot be negative");
                    }

                    if (value > 28)
                    {
                        throw new ArgumentOutOfRangeException(nameof(value), "Decimals cannot be more than 28");
                    }

                    this.decimals = value;
                }
            }

            /// <summary>
            /// Gets or sets an English name of currency.
            /// </summary>
            /// <exception cref="ArgumentException" accessor="set">
            /// <see langword="value" /> is <see langword="null" /> or contains only whitespaces.
            /// </exception>
            public string EnglishName
            {
                get
                {
                    return this.englishName;
                }

                set
                {
                    if (string.IsNullOrWhiteSpace(value))
                    {
                        throw new ArgumentException("English name cannot be null or whitespace", nameof(value));
                    }

                    this.englishName = value;
                }
            }

            /// <summary>
            /// Gets or sets a numeric code of currency defines in ISO 4217.
            /// </summary>
            public uint NumericCode { get; set; }

            /// <summary>
            /// Gets or sets a type of rounding of currency.
            /// </summary>
            /// <exception cref="ArgumentNullException" accessor="set">
            /// <see langword="value" /> is <see langword="null" />.
            /// </exception>
            public IRoundingType RoundingType
            {
                get
                {
                    return this.roundingType;
                }

                set
                {
                    if (value == null)
                    {
                        throw new ArgumentNullException(nameof(value), "Rounding type cannot be null");
                    }

                    this.roundingType = value;
                }
            }

            /// <summary>
            /// Gets or sets a symbol that indicate the currency.
            /// </summary>
            /// <exception cref="ArgumentException" accessor="set">
            /// <see langword="value" /> is <see langword="null" /> or contains only whitespaces.
            /// </exception>
            public string Symbol
            {
                get
                {
                    return this.symbol;
                }

                set
                {
                    if (string.IsNullOrWhiteSpace(value))
                    {
                        throw new ArgumentException("Symbol cannot be null or whitespace", nameof(value));
                    }

                    this.symbol = value;
                }
            }
        }
    }
}
