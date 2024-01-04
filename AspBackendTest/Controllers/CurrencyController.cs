using AspBackendTest.Application.Dtos.Requests.Currency;
using AspBackendTest.Application.Dtos.Response.Currency;
using AspBackendTest.Application.UseCase.Currency;
using Microsoft.AspNetCore.Mvc;

namespace AspBackendTest.Controllers;

[Controller]
[Route("[Controller]")]
public class CurrencyController(
    CreateCurrencyUseCase createCurrencyUseCase,
    GetAllCurrencyUseCase getAllCurrencyUseCase,
    DeleteCurrencyUseCase deleteCurrencyUseCase,
    UpdateCurrencyUseCase updateCurrencyUseCase)
    : Controller
{
    [HttpGet("")]
    public async Task<ActionResult<GetAllCurrencyResponse>> GetAllCurrency(
        [FromQuery] CurrencyQueryParameter parameter, CancellationToken cancellationToken = default)
    {
        var (currency, total) = await getAllCurrencyUseCase.Do(parameter, cancellationToken);
        return Ok(new GetAllCurrencyResponse(currency, total));
    }

    [HttpPost("")]
    public async Task<ActionResult<CreateCurrencyResponse>> CreateCurrency(
        [FromBody] CreateCurrencyRequest request, CancellationToken cancellationToken = default)
    {
        var currency = await createCurrencyUseCase.Do(request, cancellationToken);
        return Ok(new CreateCurrencyResponse(currency));
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<UpdateCurrencyResponse>> UpdateCurrency(Guid id,
        [FromBody] UpdateCurrencyRequest request, CancellationToken cancellationToken = default)
    {
        var currency = await updateCurrencyUseCase.Do(id, request, cancellationToken);
        return Ok(new UpdateCurrencyResponse(currency));
    }

    [HttpDelete("{id:guid}")]
    public async Task<ActionResult> DeleteCurrency(Guid id, CancellationToken cancellationToken = default)
    {
        await deleteCurrencyUseCase.Do(id, cancellationToken);
        return Ok();
    }
}