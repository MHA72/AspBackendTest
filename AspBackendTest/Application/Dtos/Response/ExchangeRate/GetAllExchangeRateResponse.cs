using AspBackendTest.Application.Dtos.Info;

namespace AspBackendTest.Application.Dtos.Response.ExchangeRate;

public sealed record GetAllExchangeRateResponse(List<ExchangeRateInfo> PExchangeRates, int Total);