namespace AspBackendTest.Application.Dtos.Requests.ExchangeRate;

public record ExchangeRateQueryParameter(
    DateTime? FromCreateDate,
    DateTime? ToCreateDate,
    int Skip,
    int Take,
    Guid? FromCurrency,
    Guid? ToCurrency,
    DateTime? EffectiveDate);