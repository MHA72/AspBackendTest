using AspBackendTest.Application.Dtos.Info;

namespace AspBackendTest.Application.Dtos.Response.Product;

public sealed record GetAllProductResponse(List<ProductInfo> Products, int Total);