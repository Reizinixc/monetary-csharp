namespace Reizinixc.Monetary.Tests.Rounding
{
    using NUnit.Framework;

    using Reizinixc.Monetary.Rounding;

    [TestFixture]
    [Parallelizable]
    public partial class RoundingTypeTests
    {
        [Test]
        public void ReturnInstanceOfAwayFromZeroRoundingTypeForAwayFromZeroField()
        {
            // Assert + Act
            Assert.That(RoundingType.AwayFromZero, Is.InstanceOf<AwayFromZeroRoundingType>());
        }
    }
}
