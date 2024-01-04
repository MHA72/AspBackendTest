using AspBackendTest.Application.Dtos.Info;
using AspBackendTest.Application.Dtos.Requests.Currency;
using AspBackendTest.Application.IRepositories;

namespace AspBackendTest.Application.UseCase.Currency;

public class GetAllCurrencyUseCase
{
    private readonly ICurrencyRepository _currencyRepository;

    public GetAllCurrencyUseCase(ICurrencyRepository currencyRepository)
    {
        _currencyRepository = currencyRepository;
    }

    public async Task<(List<CurrencyInfo>, int)> Do(CurrencyQueryParameter parameter,
        CancellationToken cancellationToken = default) =>
        await _currencyRepository.GetCurrencies(parameter, cancellationToken);}