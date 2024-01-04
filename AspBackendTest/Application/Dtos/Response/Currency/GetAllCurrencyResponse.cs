using AspBackendTest.Application.Dtos.Info;

namespace AspBackendTest.Application.Dtos.Response.Currency;

public sealed record GetAllCurrencyResponse(List<CurrencyInfo> Currencies, int Total);