using AspBackendTest.Application.Dtos.Info;
using AspBackendTest.Application.Dtos.Requests.PackingType;
using AspBackendTest.Application.IRepositories;

namespace AspBackendTest.Application.UseCase.PackingType;

public class GetAllPackingTypeUseCase(IPackingTypeRepository packingTypeRepository)
{
    public async Task<(List<PackingTypeInfo>, int)> Do(PackingTypeQueryParameter parameter,
        CancellationToken cancellationToken = default) =>
        await packingTypeRepository.GetPackingTypes(parameter, cancellationToken);
}