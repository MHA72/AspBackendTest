using AspBackendTest.Application.Dtos.Info;
using AspBackendTest.Infrastructure.Model;

namespace AspBackendTest.Application.Mapper;

public static class ExchangeRateMapper
{
    public static ExchangeRateInfo ToExchangeRateInfo(this ExchangeRate exchangeRate) =>
        new(exchangeRate.Id, exchangeRate.FromCurrency!.ToCurrencyInfo(), exchangeRate.ToCurrency!.ToCurrencyInfo(),
            exchangeRate.EffectiveDate, exchangeRate.MarketRate);
}