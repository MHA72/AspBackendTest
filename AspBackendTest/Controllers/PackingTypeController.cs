using AspBackendTest.Application.Dtos.Requests.PackingType;
using AspBackendTest.Application.Dtos.Response.PackingType;
using AspBackendTest.Application.UseCase.PackingType;
using Microsoft.AspNetCore.Mvc;

namespace AspBackendTest.Controllers;

[Controller]
[Route("[Controller]")]
public class PackingTypeController(
    CreatePackingTypeUseCase createPackingTypeUseCase,
    GetAllPackingTypeUseCase getAllPackingTypeUseCase,
    DeletePackingTypeUseCase deletePackingTypeUseCase,
    UpdatePackingTypeUseCase updatePackingTypeUseCase)
    : Controller
{
    [HttpGet("")]
    public async Task<ActionResult<GetAllPackingTypeResponse>> GetAllPackingType(
        [FromQuery] PackingTypeQueryParameter parameter, CancellationToken cancellationToken)
    {
        var (packingTypes, total) = await getAllPackingTypeUseCase.Do(parameter, cancellationToken);
        return Ok(new GetAllPackingTypeResponse(packingTypes, total));
    }

    [HttpPost("")]
    public async Task<ActionResult<CreatePackingTypeResponse>> CreatePackingType(
        [FromBody] CreatePackingTypeRequest request, CancellationToken cancellationToken)
    {
        var packingType = await createPackingTypeUseCase.Do(request, cancellationToken);
        return Ok(new CreatePackingTypeResponse(packingType));
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<UpdatePackingTypeResponse>> UpdatePackingType(Guid id,
        [FromBody] UpdatePackingTypeRequest request, CancellationToken cancellationToken)
    {
        var packingType = await updatePackingTypeUseCase.Do(id, request, cancellationToken);
        return Ok(new UpdatePackingTypeResponse(packingType));
    }

    [HttpDelete("{id:guid}")]
    public async Task<ActionResult> DeletePackingType(Guid id, CancellationToken cancellationToken)
    {
        await deletePackingTypeUseCase.Do(id, cancellationToken);
        return Ok();
    }
}