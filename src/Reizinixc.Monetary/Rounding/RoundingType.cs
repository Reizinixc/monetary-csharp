namespace Reizinixc.Monetary.Rounding
{
    /// <summary>
    /// Provides a static fields of rounding type for instance reusing.
    /// </summary>
    public static partial class RoundingType
    {
        /// <summary>
        /// Gets an instance of rounding type that rounded toward the nearest number that is away from zero.
        /// </summary>
        public static readonly IRoundingType AwayFromZero = new AwayFromZeroRoundingType();
    }
}
