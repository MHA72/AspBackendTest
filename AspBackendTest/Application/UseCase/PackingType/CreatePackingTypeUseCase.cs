using AspBackendTest.Application.Dtos.Requests.PackingType;
using AspBackendTest.Application.IRepositories;
using AspBackendTest.Application.Dtos.Info;

namespace AspBackendTest.Application.UseCase.PackingType;

public class CreatePackingTypeUseCase(IPackingTypeRepository packingTypeRepository)
{
    public async Task<PackingTypeInfo> Do(CreatePackingTypeRequest request,
        CancellationToken cancellationToken = default) =>
        await packingTypeRepository.AddPackingType(request, cancellationToken);
}