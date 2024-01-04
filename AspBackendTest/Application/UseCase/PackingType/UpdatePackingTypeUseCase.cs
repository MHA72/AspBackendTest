using AspBackendTest.Application.Dtos.Info;
using AspBackendTest.Application.Dtos.Requests.PackingType;
using AspBackendTest.Application.IRepositories;

namespace AspBackendTest.Application.UseCase.PackingType;

public class UpdatePackingTypeUseCase(IPackingTypeRepository packingTypeRepository)
{
    public async Task<PackingTypeInfo> Do(Guid packingTypeId, UpdatePackingTypeRequest request,
        CancellationToken cancellationToken = default) =>
        await packingTypeRepository.UpdatePackingType(packingTypeId, request, cancellationToken);
}