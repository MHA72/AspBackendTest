using AspBackendTest.Application.Dtos.Info;
using AspBackendTest.Infrastructure.Model;

namespace AspBackendTest.Application.Mapper;

public static class ProductMapper
{
    public static ProductInfo ToProductInfo(this Product product) =>
        new(product.Id, product.Name, product.TotalPrice, product.PriceCurrency, product.Currency!.ToCurrencyInfo(),
            product.Color, product.SizeNumber, product.SizeType);
}