using System.ComponentModel.DataAnnotations;

namespace AspBackendTest.Application.Dtos.Requests.PackingType;

public sealed record CreatePackingTypeRequest(
    [Required(AllowEmptyStrings = false)] string Name,
    [Range(double.Epsilon, double.MaxValue, ErrorMessage = "Amount must be positive")] decimal Price);