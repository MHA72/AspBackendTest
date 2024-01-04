using AspBackendTest.Application.IRepositories;

namespace AspBackendTest.Application.UseCase.ExchangeRate;

public class DeleteExchangeRateUseCase(IExchangeRateRepository exchangeRateRepository)
{
    public async Task Do(Guid id, CancellationToken cancellationToken) =>
        await exchangeRateRepository.DeleteExchangeRate(id, cancellationToken);
}