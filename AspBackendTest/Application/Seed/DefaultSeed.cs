using AspBackendTest.Application.IRepositories;
using AspBackendTest.Infrastructure.Model;

namespace AspBackendTest.Application.Seed;

public sealed class DefaultSeed(ICurrencyRepository currencyRepository)
{
    public void Seed()
    {
        currencyRepository.Migrate();

        var currency = currencyRepository.GetIRRCurrency();
        if (currency == null)
        {
            currency = new Currency("ریال ایران", "Rial", "IRR");
            currencyRepository.AddCurrencySync(currency);
        }
    }
}