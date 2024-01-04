using System.ComponentModel.DataAnnotations;
using AspBackendTest.Infrastructure.Model.Enum;

namespace AspBackendTest.Application.Dtos.Requests.Product;

public sealed record CreateProductRequest(
    [Required(AllowEmptyStrings = false)] string Name,
    [Range(double.Epsilon, double.MaxValue, ErrorMessage = "Amount must be positive")] decimal Price,
    Guid CurrencyId,
    [Required(AllowEmptyStrings = false)] string Color,
    Size? SizeType,
    float? SizeNumber,
    Guid PackingTypeId,
    IFormFile File);