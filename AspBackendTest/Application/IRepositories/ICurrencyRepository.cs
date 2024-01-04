using AspBackendTest.Application.Dtos.Info;
using AspBackendTest.Application.Dtos.Requests.Currency;
using AspBackendTest.Infrastructure.Model;

namespace AspBackendTest.Application.IRepositories;

public interface ICurrencyRepository
{
    void Migrate();
    Task<(List<CurrencyInfo>, int)> GetCurrencies(CurrencyQueryParameter parameter,
        CancellationToken cancellationToken);

    Task<Currency> GetCurrency(Guid currencyId, CancellationToken cancellationToken);
    Task<Currency?> GetCurrencyByCode(string code, CancellationToken cancellationToken);
    Currency? GetIRRCurrency();
    Task<CurrencyInfo> AddCurrency(Currency currency, CancellationToken cancellationToken);
    void AddCurrencySync(Currency currency);
    Task<CurrencyInfo> UpdateCurrency(Guid id, UpdateCurrencyRequest request, CancellationToken cancellationToken);
    Task RemoveCurrency(Guid id, CancellationToken cancellationToken = default);
}