namespace Reizinixc.Monetary.Rounding
{
    using System;

    /// <summary>
    /// Represents a rounding type that rounded toward the nearest number that is away from zero.
    /// </summary>
    public sealed class AwayFromZeroRoundingType : IRoundingType
    {
        /// <summary>
        /// A hash code for comparing of <see cref="AwayFromZeroRoundingType" />.
        /// </summary>
        private static readonly int HashCode = nameof(AwayFromZeroRoundingType).GetHashCode();

        /// <inheritdoc />
        public bool Equals(IRoundingType other)
        {
            return other is AwayFromZeroRoundingType;
        }

        /// <inheritdoc />
        public override bool Equals(object obj)
        {
            return obj is AwayFromZeroRoundingType;
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            return HashCode;
        }

        /// <inheritdoc />
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="decimals" /> is less than 0 or greater than 28.
        /// </exception>
        /// <exception cref="OverflowException">
        /// The result is outside the range of a <see cref="T:System.Decimal" />.
        /// </exception>
        public decimal Round(decimal value, int decimals)
        {
            return Math.Round(value, decimals, MidpointRounding.AwayFromZero);
        }
    }
}
