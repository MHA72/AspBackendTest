using AspBackendTest.Infrastructure.Model.Enum;

namespace AspBackendTest.Application.Dtos.Requests.Product;

public enum SortType
{
    Asc,
    Desc
}

public record SortParameter(string ColumnName, SortType Type);

public record ProductQueryParameter(DateTime? FromCreateDate, DateTime? ToCreateDate, int Skip, int Take,
    List<SortParameter>? Sorts, decimal? Price, string? Name, string? Color, Size? Size,
    Guid? PackingTypeId, Guid? CurrencyId);