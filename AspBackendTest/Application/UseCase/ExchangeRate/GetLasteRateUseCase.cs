using AspBackendTest.Application.Dtos.Info;
using AspBackendTest.Application.Dtos.Requests.ExchangeRate;
using AspBackendTest.Application.IRepositories;
using AspBackendTest.Application.Mapper;

namespace AspBackendTest.Application.UseCase.ExchangeRate;

public sealed class GetLastRateUseCase
{
    private readonly IExchangeRateRepository _exchangeRateRepository;

    public GetLastRateUseCase(
        IExchangeRateRepository exchangeRateRepository)
    {
        _exchangeRateRepository = exchangeRateRepository;
    }

    public async Task<ExchangeRateInfo?> Do(GetLastRateRequest request,
        CancellationToken cancellationToken)
    {
        var rate = await _exchangeRateRepository.GetLastExchangeRate(request.FromCurrencyId, request.ToCurrencyId,
            request.Time, cancellationToken);
        return rate?.ToExchangeRateInfo();
    }
}