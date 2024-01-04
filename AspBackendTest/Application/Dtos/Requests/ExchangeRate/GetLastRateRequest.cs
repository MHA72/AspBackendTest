namespace AspBackendTest.Application.Dtos.Requests.ExchangeRate;

public sealed record GetLastRateRequest(Guid FromCurrencyId, Guid ToCurrencyId, DateTime Time);