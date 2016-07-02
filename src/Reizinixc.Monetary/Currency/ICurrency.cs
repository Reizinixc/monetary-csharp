namespace Reizinixc.Monetary.Currency
{
    using Reizinixc.Monetary.Rounding;

    /// <summary>
    /// Defines a properties and methods for accepted form of monetary.
    /// </summary>
    public interface ICurrency
    {
        /// <summary>
        /// Gets an alphabetic code of currency defines in ISO 4217.
        /// </summary>
        string AlphabeticCode { get; }

        /// <summary>
        /// Gets a number of currency decimals or exponents.
        /// </summary>
        int Decimals { get; }

        /// <summary>
        /// Gets an English name of currency.
        /// </summary>
        string EnglishName { get; }

        /// <summary>
        /// Gets a numeric code of currency defines in ISO 4217.
        /// </summary>
        uint NumericCode { get; }

        /// <summary>
        /// Gets an instance of type of monetary rounding.
        /// </summary>
        IRoundingType RoundingType { get; }

        /// <summary>
        /// Gets a symbol that indicate the currency.
        /// </summary>
        string Symbol { get; }

        /// <summary>
        /// Rounds a value to a specified value of currency rounding type.
        /// </summary>
        /// <param name="value">A value to be rounded.</param>
        /// <returns>Rounded value that rounded from rounding type of currency.</returns>
        decimal Round(decimal value);
    }
}
