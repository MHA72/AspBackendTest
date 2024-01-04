using AspBackendTest.Application.Dtos.Info;
using AspBackendTest.Application.Dtos.Requests.Currency;
using AspBackendTest.Application.IRepositories;

namespace AspBackendTest.Application.UseCase.Currency;

public class CreateCurrencyUseCase
{
    private readonly ICurrencyRepository _currencyRepository;

    public CreateCurrencyUseCase(ICurrencyRepository currencyRepository)
    {
        _currencyRepository = currencyRepository;
    }

    public async Task<CurrencyInfo> Do(CreateCurrencyRequest request, CancellationToken cancellationToken)
    {
        var currency = new Infrastructure.Model.Currency(request.Name, request.EnglishName, request.Code);
        return await _currencyRepository.AddCurrency(currency, cancellationToken);
    }
}