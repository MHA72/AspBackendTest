using AspBackendTest.Application.Dtos.Requests.Product;
using AspBackendTest.Application.Dtos.Response.Product;
using AspBackendTest.Application.UseCase.Product;
using Microsoft.AspNetCore.Mvc;

namespace AspBackendTest.Controllers;

[Controller]
[Route("[Controller]")]
public class ProductController(
    CreateProductUseCase createProductUseCase,
    GetAllProductUseCase getAllProductUseCase,
    DeleteProductUseCase deleteProductUseCase,
    UpdateProductUseCase updateProductUseCase)
    : Controller
{
    [HttpGet("")]
    public async Task<ActionResult<GetAllProductResponse>> GetAllProduct(
        [FromQuery] ProductQueryParameter parameter, CancellationToken cancellationToken)
    {
        var (products, total) = await getAllProductUseCase.Do(parameter, cancellationToken);
        return Ok(new GetAllProductResponse(products, total));
    }

    [HttpPost("")]
    public async Task<ActionResult<CreateProductResponse>> CreateProduct(
        [FromForm] CreateProductRequest request, CancellationToken cancellationToken)
    {
        var product = await createProductUseCase.Do(request, cancellationToken);
        return Ok(new CreateProductResponse(product));
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<UpdateProductResponse>> UpdateProduct(Guid id,
        [FromBody] UpdateProductRequest request, CancellationToken cancellationToken)
    {
        var product = await updateProductUseCase.Do(id, request, cancellationToken);
        return Ok(new UpdateProductResponse(product));
    }

    [HttpDelete("{id:guid}")]
    public async Task<ActionResult> DeleteProduct(Guid id,
        CancellationToken cancellationToken)
    {
        await deleteProductUseCase.Do(id, cancellationToken);
        return Ok();
    }
}