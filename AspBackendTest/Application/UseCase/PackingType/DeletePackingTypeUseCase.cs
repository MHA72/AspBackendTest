using AspBackendTest.Application.IRepositories;

namespace AspBackendTest.Application.UseCase.PackingType;

public class DeletePackingTypeUseCase(IPackingTypeRepository packingTypeRepository)
{
    public async Task Do(Guid id, CancellationToken cancellationToken = default) =>
        await packingTypeRepository.RemovePackingType(id, cancellationToken);
}