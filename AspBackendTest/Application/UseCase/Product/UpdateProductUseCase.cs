using AspBackendTest.Application.Dtos.Requests.Product;
using AspBackendTest.Application.IRepositories;
using AspBackendTest.Application.Dtos.Info;

namespace AspBackendTest.Application.UseCase.Product;

public class UpdateProductUseCase(
    IProductRepository productRepository,
    ICurrencyRepository currencyRepository,
    IExchangeRateRepository exchangeRateRepository)
{
    public async Task<ProductInfo> Do(Guid id, UpdateProductRequest request,
        CancellationToken cancellationToken)
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

        totalPrice *= rate!.MarketRate;

        return await productRepository.UpdateProduct(id, request, totalPrice, cancellationToken);
    }
}