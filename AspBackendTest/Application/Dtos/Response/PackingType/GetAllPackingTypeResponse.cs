using AspBackendTest.Application.Dtos.Info;

namespace AspBackendTest.Application.Dtos.Response.PackingType;

public sealed record GetAllPackingTypeResponse(List<PackingTypeInfo> PackingTypes, int Total);