namespace AspBackendTest.Application.Dtos.Requests.Currency;

public record CurrencyQueryParameter(
    DateTime? FromCreateDate,
    DateTime? ToCreateDate,
    int Skip,
    int Take,
    string? Name,
    string? EnglishName,
    string? Code);