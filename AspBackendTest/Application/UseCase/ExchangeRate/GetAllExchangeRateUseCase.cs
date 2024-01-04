using AspBackendTest.Application.Dtos.Info;
using AspBackendTest.Application.Dtos.Requests.ExchangeRate;
using AspBackendTest.Application.IRepositories;

namespace AspBackendTest.Application.UseCase.ExchangeRate;

public class GetAllExchangeRateUseCase(IExchangeRateRepository exchangeRateRepository)
{
    public Task<(List<ExchangeRateInfo>, int)> Do(ExchangeRateQueryParameter parameter,
        CancellationToken cancellationToken) =>
        exchangeRateRepository.GetExchangeRates(parameter,
            cancellationToken);
}