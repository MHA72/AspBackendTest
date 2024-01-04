using AspBackendTest.Application.Dtos.Info;
using AspBackendTest.Infrastructure.Model;

namespace AspBackendTest.Application.Mapper;

public static class CurrencyMapper
{
    public static CurrencyInfo ToCurrencyInfo(this Currency currency) =>
        new(currency.Id, currency.Name, currency.EnglishName, currency.Code);
}