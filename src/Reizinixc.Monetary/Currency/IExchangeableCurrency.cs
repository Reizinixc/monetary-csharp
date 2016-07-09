namespace Reizinixc.Monetary.Currency
{
    using System;

    /// <summary>
    /// Defines a type that represents an accepted form of monetary which able to exchange.
    /// </summary>
    public interface IExchangeableCurrency : ICurrency, IExchangeable<decimal, decimal, decimal>, IEquatable<IExchangeableCurrency>
    {
        // Do nothing.
    }
}
