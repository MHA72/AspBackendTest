using AspBackendTest.Application.Dtos.Requests.ExchangeRate;
using AspBackendTest.Application.Dtos.Info;
using AspBackendTest.Infrastructure.Model;

namespace AspBackendTest.Application.IRepositories;

public interface IExchangeRateRepository
{
    Task<(List<ExchangeRateInfo>, int)> GetExchangeRates(ExchangeRateQueryParameter parameter,
        CancellationToken cancellationToken = default);

    Task<ExchangeRate?> GetLastExchangeRate(Guid fromCurrencyId, Guid toCurrencyId, DateTime date,
        CancellationToken cancellationToken = default);
    Task<ExchangeRateInfo> AddExchangeRate(CreateExchangeRateRequest request,
        CancellationToken cancellationToken = default);
    Task DeleteExchangeRate(Guid id, CancellationToken cancellationToken = default);
}