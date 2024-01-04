namespace AspBackendTest.Application.Dtos.Requests.ExchangeRate;

public sealed record CreateExchangeRateRequest(
    Guid FromCurrencyId,
    Guid ToCurrencyId,
    DateTime EffectiveDate,
    decimal MarketRate);