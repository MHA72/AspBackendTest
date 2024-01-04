using AspBackendTest.Application.Dtos.Info;
using AspBackendTest.Application.Dtos.Requests.Currency;
using AspBackendTest.Application.IRepositories;
using AspBackendTest.Application.Mapper;
using AspBackendTest.Infrastructure.Context;
using AspBackendTest.Infrastructure.Model;
using Microsoft.EntityFrameworkCore;

namespace AspBackendTest.Infrastructure.Repositories;

public class CurrencyRepository : ICurrencyRepository
{
    private readonly ProductContext _authContext;

    public CurrencyRepository(ProductContext authContext)
    {
        _authContext = authContext;
    }

    public void Migrate()
    {
        _authContext.Database.Migrate();
    }
    public async Task<(List<CurrencyInfo>, int)> GetCurrencies(CurrencyQueryParameter parameter,
        CancellationToken cancellationToken)
    {
        var queryableCurrency = _authContext.Currencies!.AsQueryable();

        if (!string.IsNullOrEmpty(parameter.Name))
            queryableCurrency = queryableCurrency.Where(currency =>
                currency.Name.ToLower().Contains(parameter.Name.ToLower()));

        if (!string.IsNullOrEmpty(parameter.EnglishName))
            queryableCurrency = queryableCurrency.Where(currency =>
                currency.EnglishName.ToLower().Contains(parameter.EnglishName.ToLower()));

        if (!string.IsNullOrEmpty(parameter.Code))
            queryableCurrency = queryableCurrency.Where(currency =>
                currency.Code.ToLower().Contains(parameter.Code.ToLower()));


        var total = await queryableCurrency.CountAsync(cancellationToken);

        queryableCurrency = queryableCurrency.OrderByDescending(currency => currency.UpdateTime);

        if (parameter.Skip != 0) queryableCurrency = queryableCurrency.Skip(parameter.Skip);
        if (parameter.Take != 0) queryableCurrency = queryableCurrency.Take(parameter.Take);

        return (
            await queryableCurrency.Select(currency => currency.ToCurrencyInfo())
                .ToListAsync(cancellationToken), total);
    }

    public async Task<Currency> GetCurrency(Guid currencyId, CancellationToken cancellationToken = default)
    {
        return await _authContext.Currencies!
            .Where(currency => currency.Id == currencyId)
            .FirstAsync(cancellationToken);
    }

    public async Task<Currency?> GetCurrencyByCode(string code, CancellationToken cancellationToken)
    {
        return await _authContext.Currencies!
            .Where(currency => currency.Code.ToLower() == code.ToLower())
            .FirstOrDefaultAsync(cancellationToken);
    }

    public Currency? GetIRRCurrency()
    {
        return _authContext.Currencies!.FirstOrDefault(currency => currency.Code == "IRR");
    }

    public async Task<CurrencyInfo> AddCurrency(Currency currency, CancellationToken cancellationToken = default)
    {
        await _authContext.Currencies!.AddAsync(currency, cancellationToken);
        await _authContext.SaveChangesAsync(cancellationToken);
        return currency.ToCurrencyInfo();
    }

    public void AddCurrencySync(Currency currency)
    {
        _authContext.Currencies!.Add(currency);
        _authContext.SaveChanges();
    }

    public async Task<CurrencyInfo> UpdateCurrency(Guid id, UpdateCurrencyRequest request,
        CancellationToken cancellationToken = default)
    {
        var currency = await _authContext.Currencies!.FirstAsync(currency => currency.Id == id, cancellationToken);

        currency.UpdateTime = DateTime.Now;
        currency.Name = request.Name;
        currency.EnglishName = request.EnglishName;
        currency.Code = request.Code;
        await _authContext.SaveChangesAsync(cancellationToken);

        return currency.ToCurrencyInfo();
    }

    public async Task<bool> CheckForUsage(Guid id, CancellationToken cancellationToken = default)
    {
        return await _authContext.ExchangeRates!.AnyAsync(rate =>
            rate.FromCurrencyId == id || rate.ToCurrencyId == id, cancellationToken);
    }

    public async Task RemoveCurrency(Guid id, CancellationToken cancellationToken = default)
    {
        var currency =
            await _authContext.Currencies!.FirstAsync(currency => currency.Id == id, cancellationToken);

        currency.UpdateTime = DateTime.Now;
        currency.IsDeleted = true;

        await _authContext.SaveChangesAsync(cancellationToken);
    }
}