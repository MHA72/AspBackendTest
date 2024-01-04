using AspBackendTest.Application.Dtos.Info;
using AspBackendTest.Application.Dtos.Requests.Product;

namespace AspBackendTest.Application.IRepositories;

public interface IProductRepository
{
    Task<(List<ProductInfo>, int)> GetProducts(ProductQueryParameter parameter,
        CancellationToken cancellationToken = default);

    Task<ProductInfo> AddProduct(CreateProductRequest request, decimal totalPrice,
        CancellationToken cancellationToken = default);

    Task<ProductInfo> UpdateProduct(Guid id, UpdateProductRequest request, decimal totalPrice,
        CancellationToken cancellationToken = default);

    Task RemoveProduct(Guid id, CancellationToken cancellationToken = default);
}