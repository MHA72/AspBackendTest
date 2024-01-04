using AspBackendTest.Application.Dtos.Info;
using AspBackendTest.Application.Dtos.Requests.PackingType;
using AspBackendTest.Infrastructure.Model;

namespace AspBackendTest.Application.IRepositories;

public interface IPackingTypeRepository
{
    Task<(List<PackingTypeInfo>, int)> GetPackingTypes(PackingTypeQueryParameter parameter,
        CancellationToken cancellationToken = default);
    Task<PackingTypeInfo> AddPackingType(CreatePackingTypeRequest request,
        CancellationToken cancellationToken = default);

    Task<PackingTypeInfo> UpdatePackingType(Guid id, UpdatePackingTypeRequest request,
        CancellationToken cancellationToken = default);
    Task<PackingType> GetPackingType(Guid id, CancellationToken cancellationToken = default);
    Task RemovePackingType(Guid id, CancellationToken cancellationToken = default);
}