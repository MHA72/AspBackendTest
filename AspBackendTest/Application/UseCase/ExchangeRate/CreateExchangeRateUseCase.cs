using AspBackendTest.Application.Dtos.Info;
using AspBackendTest.Application.Dtos.Requests.ExchangeRate;
using AspBackendTest.Application.IRepositories;

namespace AspBackendTest.Application.UseCase.ExchangeRate;

public class CreateExchangeRateUseCase(IExchangeRateRepository exchangeRateRepository)
{
    public async Task<ExchangeRateInfo> Do(CreateExchangeRateRequest request,
        CancellationToken cancellationToken) =>
        await exchangeRateRepository.AddExchangeRate(request, cancellationToken);
}