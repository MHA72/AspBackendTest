using AspBackendTest.Application.Dtos.Info;
using AspBackendTest.Application.Dtos.Requests.PackingType;
using AspBackendTest.Application.IRepositories;
using AspBackendTest.Application.Mapper;
using AspBackendTest.Infrastructure.Context;
using AspBackendTest.Infrastructure.Model;
using Microsoft.EntityFrameworkCore;

namespace AspBackendTest.Infrastructure.Repositories;

public class PackingTypeRepository : IPackingTypeRepository
{
    private readonly ProductContext _authContext;

    public PackingTypeRepository(ProductContext authContext)
    {
        _authContext = authContext;
    }

    public async Task<(List<PackingTypeInfo>, int)> GetPackingTypes(PackingTypeQueryParameter parameter,
        CancellationToken cancellationToken = default)
    {
        var queryablePackingType = _authContext.PackingTypes!.AsQueryable();

        if (!string.IsNullOrEmpty(parameter.Name))
            queryablePackingType =
                queryablePackingType.Where(type => type.Name.ToLower().Contains(parameter.Name.ToLower()));

        if (parameter.Price != 0)
            queryablePackingType = queryablePackingType.Where(type =>
                type.Price == parameter.Price);

        var total = await queryablePackingType.CountAsync(cancellationToken);

        queryablePackingType = queryablePackingType.OrderBy(type => type.UpdateTime);

        if (parameter.Skip != 0) queryablePackingType = queryablePackingType.Skip(parameter.Skip);
        if (parameter.Take != 0) queryablePackingType = queryablePackingType.Take(parameter.Take);

        return (
            await queryablePackingType.Select(type => type.ToPackingTypeInfo())
                .ToListAsync(cancellationToken), total);
    }

    public async Task<PackingTypeInfo> AddPackingType(CreatePackingTypeRequest request,
        CancellationToken cancellationToken = default)
    {
        var packingType = new PackingType(request.Name, request.Price);
        await _authContext.PackingTypes!.AddAsync(packingType, cancellationToken);
        await _authContext.SaveChangesAsync(cancellationToken);

        return packingType.ToPackingTypeInfo();
    }

    public async Task<PackingTypeInfo> UpdatePackingType(Guid id, UpdatePackingTypeRequest request,
        CancellationToken cancellationToken = default)
    {
        var packingType = await _authContext.PackingTypes!
            .FirstAsync(packingType => packingType.Id == id, cancellationToken);

        packingType.Name = request.Name;
        packingType.Price = request.Price;

        await _authContext.SaveChangesAsync(cancellationToken);

        return await _authContext.PackingTypes!
            .Where(packingType => packingType.Id == id)
            .Select(packingType => packingType.ToPackingTypeInfo())
            .FirstAsync(cancellationToken);
    }

    public async Task<PackingType> GetPackingType(Guid id, CancellationToken cancellationToken = default)
    {
        return await _authContext.PackingTypes!
            .FirstAsync(packingType => packingType.Id == id, cancellationToken);
    }

    public async Task RemovePackingType(Guid id, CancellationToken cancellationToken = default)
    {
        var packingType = await _authContext.PackingTypes!
            .FirstAsync(packingType => packingType.Id == id, cancellationToken);

        packingType.UpdateTime = DateTime.Now;
        packingType.IsDeleted = true;

        await _authContext.SaveChangesAsync(cancellationToken);
    }
}