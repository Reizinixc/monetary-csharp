namespace Reizinixc.Monetary.Rounding
{
    using System;

    /// <summary>
    /// Defines method to round values using number of decimal.
    /// </summary>
    public interface IRoundingType : IEquatable<IRoundingType>
    {
        /// <summary>
        /// Rounds a value to a specified value of fractional digits.
        /// </summary>
        /// <param name="value">A value to be rounded.</param>
        /// <param name="decimals">The number of decimal places in the return value.</param>
        /// <returns>Rounded value that used the rounding strategy.</returns>
        decimal Round(decimal value, int decimals);
    }
}
