
namespace AspBackendTest.Application.Dtos.Info;

public sealed record CurrencyInfo(
    Guid Id,
    string Name,
    string EnglishName,
    string Code
);