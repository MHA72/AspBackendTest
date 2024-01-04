using AspBackendTest.Infrastructure.Model.Enum;

namespace AspBackendTest.Application.Dtos.Info;

public sealed record ProductInfo(
    Guid Id,
    string Name,
    decimal TotalPrice,
    decimal PriceCurrency,
    CurrencyInfo? Currency,
    string Color,
    float? SizeNumber,
    Size? SizeType
);