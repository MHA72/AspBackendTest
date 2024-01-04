using AspBackendTest.Application.Dtos.Requests.ExchangeRate;
using AspBackendTest.Application.IRepositories;
using AspBackendTest.Infrastructure.Context;
using AspBackendTest.Application.Dtos.Info;
using AspBackendTest.Infrastructure.Model;
using AspBackendTest.Application.Mapper;
using Microsoft.EntityFrameworkCore;

namespace AspBackendTest.Infrastructure.Repositories;

public class ExchangeRateRepository : IExchangeRateRepository
{
    private readonly ProductContext _authContext;

    public ExchangeRateRepository(ProductContext authContext)
    {
        _authContext = authContext;
    }

    public async Task<(List<ExchangeRateInfo>, int)> GetExchangeRates(ExchangeRateQueryParameter parameter,
        CancellationToken cancellationToken)
    {
        var queryableExchangeRate = _authContext.ExchangeRates!
            .Include(rate => rate.FromCurrency)
            .Include(rate => rate.ToCurrency)
            .AsQueryable();

        var total = await queryableExchangeRate.CountAsync(cancellationToken);

        queryableExchangeRate = queryableExchangeRate.OrderByDescending(rate => rate.EffectiveDate);

        if (parameter.Skip != 0) queryableExchangeRate = queryableExchangeRate.Skip(parameter.Skip);
        if (parameter.Take != 0) queryableExchangeRate = queryableExchangeRate.Take(parameter.Take);

        return (await queryableExchangeRate.Select(exchangeRate => exchangeRate.ToExchangeRateInfo())
            .ToListAsync(cancellationToken), total);
    }

    public Task<ExchangeRate?> GetLastExchangeRate(Guid fromCurrencyId, Guid toCurrencyId, DateTime date,
        CancellationToken cancellationToken)
    {
        date = date.ToUniversalTime();
        return _authContext.ExchangeRates!
            .Include(rate => rate.FromCurrency)
            .Include(rate => rate.ToCurrency)
            .OrderByDescending(rate => rate.InsertTime)
            .Where(rate => rate.FromCurrencyId == fromCurrencyId && rate.ToCurrencyId == toCurrencyId)
            .Where(rate => rate.EffectiveDate <= date)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<ExchangeRateInfo> AddExchangeRate(CreateExchangeRateRequest request,
        CancellationToken cancellationToken)
    {
        var exchangeRate = new ExchangeRate(request.FromCurrencyId, request.ToCurrencyId, request.EffectiveDate,
            request.MarketRate);
        await _authContext.ExchangeRates!.AddAsync(exchangeRate, cancellationToken);
        await _authContext.SaveChangesAsync(cancellationToken);

        return await _authContext.ExchangeRates
            .Include(rate => rate.FromCurrency)
            .Include(rate => rate.ToCurrency)
            .Where(rate => rate.Id == exchangeRate.Id)
            .Select(rate => rate.ToExchangeRateInfo())
            .FirstAsync(cancellationToken);
    }

    public async Task DeleteExchangeRate(Guid id, CancellationToken cancellationToken)
    {
        var exchange = await _authContext.ExchangeRates!
            .FirstAsync(exchangeRate => exchangeRate.Id == id, cancellationToken);

        exchange.UpdateTime = DateTime.Now;
        exchange.IsDeleted = true;

        await _authContext.SaveChangesAsync(cancellationToken);
    }
}