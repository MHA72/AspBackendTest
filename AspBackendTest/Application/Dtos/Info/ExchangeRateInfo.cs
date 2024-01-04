namespace AspBackendTest.Application.Dtos.Info;

public sealed record ExchangeRateInfo(
    Guid Id,
    CurrencyInfo? FromCurrency,
    CurrencyInfo? ToCurrency,
    DateTime EffectiveDate,
    decimal MarketRate
);