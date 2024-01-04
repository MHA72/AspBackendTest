using AspBackendTest.Application.Dtos.Requests.ExchangeRate;
using AspBackendTest.Application.Dtos.Response.ExchangeRate;
using AspBackendTest.Application.UseCase.ExchangeRate;
using Microsoft.AspNetCore.Mvc;

namespace AspBackendTest.Controllers;

[Controller]
[Route("[Controller]")]
public class ExchangeRateController(
    CreateExchangeRateUseCase createExchangeRateUseCase,
    GetAllExchangeRateUseCase getAllExchangeRateUseCase,
    DeleteExchangeRateUseCase deleteExchangeRateUseCase,
    GetLastRateUseCase lastRateUseCase)
    : Controller
{
    [HttpGet("")]
    public async Task<ActionResult<GetAllExchangeRateResponse>> GetAllExchangeRate(
        [FromQuery] ExchangeRateQueryParameter parameter, CancellationToken cancellationToken)
    {
        var (exchangeRates, total) = await getAllExchangeRateUseCase.Do(parameter, cancellationToken);
        return Ok(new GetAllExchangeRateResponse(exchangeRates, total));
    }

    [HttpPost("")]
    public async Task<ActionResult<CreateExchangeRateResponse>> CreateExchangeRate(
        [FromBody] CreateExchangeRateRequest request, CancellationToken cancellationToken)
    {
        var exchangeRate = await createExchangeRateUseCase.Do(request, cancellationToken);
        return Ok(new CreateExchangeRateResponse(exchangeRate));
    }

    [HttpDelete("{id:guid}")]
    public async Task<ActionResult> DeleteExchangeRate(Guid id, CancellationToken cancellationToken)
    {
        await deleteExchangeRateUseCase.Do(id, cancellationToken);
        return Ok();
    }


    
    [HttpGet("LastRate")]
    public async Task<ActionResult<GetLastRateResponse>> GetLastRate(
        [FromQuery] GetLastRateRequest request, CancellationToken cancellationToken)
    {
        var rate = await lastRateUseCase.Do(request , cancellationToken);
        return Ok(new GetLastRateResponse(rate));
    }
}