using ProductInfo = AspBackendTest.Application.Dtos.Info.ProductInfo;
using AspBackendTest.Application.Dtos.Requests.Product;
using AspBackendTest.Application.IRepositories;

namespace AspBackendTest.Application.UseCase.Product;

public class CreateProductUseCase(
    IProductRepository productRepository,
    ICurrencyRepository currencyRepository,
    IPackingTypeRepository packingTypeRepository,
    IExchangeRateRepository exchangeRateRepository)
{
    public async Task<ProductInfo> Do(CreateProductRequest request, CancellationToken cancellationToken)
    {
        var currency = await currencyRepository.GetCurrency(request.CurrencyId, cancellationToken);
        var currencyIRR = await currencyRepository.GetCurrencyByCode("IRR", cancellationToken);
        var rate = await exchangeRateRepository.GetLastExchangeRate(request.CurrencyId, currencyIRR!.Id,
            DateTime.Now, cancellationToken);
        var totalPrice = request.Price;
        if (currency.Code != "IRR")
        {
            if (rate == null)
            {
                throw new BadHttpRequestException(
                    $"Please Define ExchangeRate From Currency {currency.EnglishName} to Currency {currencyIRR.EnglishName}");
            }
        }

        var packingType = await packingTypeRepository.GetPackingType(request.PackingTypeId, cancellationToken);

        totalPrice *= rate!.MarketRate + packingType.Price;

        return await productRepository.AddProduct(request, totalPrice, cancellationToken);
    }
}