using AspBackendTest.Application.Dtos.Info;
using AspBackendTest.Application.Dtos.Requests.Currency;
using AspBackendTest.Application.IRepositories;

namespace AspBackendTest.Application.UseCase.Currency;

public class UpdateCurrencyUseCase
{
    private readonly ICurrencyRepository _currencyRepository;

    public UpdateCurrencyUseCase(ICurrencyRepository currencyRepository)
    {
        _currencyRepository = currencyRepository;
    }

    public async Task<CurrencyInfo> Do(Guid currencyId, UpdateCurrencyRequest request,
        CancellationToken cancellationToken = default) =>
        await _currencyRepository.UpdateCurrency(currencyId, request, cancellationToken);
}