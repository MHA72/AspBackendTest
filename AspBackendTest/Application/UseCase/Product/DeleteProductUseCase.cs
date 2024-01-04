using AspBackendTest.Application.IRepositories;

namespace AspBackendTest.Application.UseCase.Product;

public class DeleteProductUseCase(IProductRepository productRepository)
{
    public async Task Do(Guid id, CancellationToken cancellationToken) =>
        await productRepository.RemoveProduct(id, cancellationToken);  
}