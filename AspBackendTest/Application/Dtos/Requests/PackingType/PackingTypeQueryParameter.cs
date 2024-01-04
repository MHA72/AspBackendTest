namespace AspBackendTest.Application.Dtos.Requests.PackingType;

public record PackingTypeQueryParameter(
    DateTime? FromCreateDate,
    DateTime? ToCreateDate,
    int Skip,
    int Take,
    decimal? Price,
    string? Name);