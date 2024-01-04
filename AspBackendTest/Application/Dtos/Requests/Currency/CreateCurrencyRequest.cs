using System.ComponentModel.DataAnnotations;

namespace AspBackendTest.Application.Dtos.Requests.Currency;

public sealed record CreateCurrencyRequest(
    [Required(AllowEmptyStrings = false)] string Name,
    [Required(AllowEmptyStrings = false)] string EnglishName,
    [Required(AllowEmptyStrings = false)] string Code);