using AspBackendTest.Application.IRepositories;

namespace AspBackendTest.Application.UseCase.Currency;

public class DeleteCurrencyUseCase
{
    private readonly ICurrencyRepository _currencyRepository;

    public DeleteCurrencyUseCase(ICurrencyRepository currencyRepository)
    {
        _currencyRepository = currencyRepository;
    }

    public async Task Do(Guid id, CancellationToken cancellationToken = default)
    {
        await _currencyRepository.RemoveCurrency(id, cancellationToken);
    }
}