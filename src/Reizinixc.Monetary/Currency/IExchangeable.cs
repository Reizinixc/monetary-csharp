namespace Reizinixc.Monetary.Currency
{
    using System;

    /// <summary>
    /// Defines generalized methods that adds ability to do exchange between <typeparamref name="TFrom" />
    /// and <typeparamref name="TTo" /> using <typeparamref name="TRate" /> as a exchange rate.
    /// </summary>
    /// <typeparam name="TFrom">Type of object to do exchange.</typeparam>
    /// <typeparam name="TTo">Type of object that get from doing exchange.</typeparam>
    /// <typeparam name="TRate">Type of exchange rate.</typeparam>
    public interface IExchangeable<TFrom, TTo, TRate>
    {
        /// <summary>
        /// Gets an exchange rate in <typeparamref name="TRate" />.
        /// </summary>
        TRate ExchangeRate { get; }

        /// <summary>
        /// Exchanges an object to the provided exchangeable item type.
        /// </summary>
        /// <param name="value">A value to be exchanged.</param>
        /// <param name="target">A target exchangeable type to be exchange to.</param>
        /// <returns>An exchanged value that exchanged from provided value.</returns>
        TTo Exchange(TFrom value, IExchangeable<TFrom, TTo, TRate> target);
    }
}
