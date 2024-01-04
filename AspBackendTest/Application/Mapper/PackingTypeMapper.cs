using AspBackendTest.Application.Dtos.Info;
using AspBackendTest.Infrastructure.Model;

namespace AspBackendTest.Application.Mapper;

public static class PackingTypeMapper
{
    public static PackingTypeInfo ToPackingTypeInfo(this PackingType packingType) =>
        new(packingType.Id, packingType.Name, packingType.Price);
}