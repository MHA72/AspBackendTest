using ProductInfo = AspBackendTest.Application.Dtos.Info.ProductInfo;
using AspBackendTest.Application.Dtos.Requests.Product;
using AspBackendTest.Application.IRepositories;

namespace AspBackendTest.Application.UseCase.Product;

public class GetAllProductUseCase(IProductRepository productRepository)
{
    public async Task<(List<ProductInfo>, int)> Do(ProductQueryParameter parameter,
        CancellationToken cancellationToken) =>
        await productRepository.GetProducts(parameter, cancellationToken);
}