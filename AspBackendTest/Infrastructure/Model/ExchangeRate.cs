namespace AspBackendTest.Infrastructure.Model;

public class ExchangeRate : BaseEntity
{
    public ExchangeRate(Guid fromCurrencyId, Guid toCurrencyId, DateTime effectiveDate, decimal marketRate)
    {
        FromCurrencyId = fromCurrencyId;
        ToCurrencyId = toCurrencyId;
        EffectiveDate = effectiveDate;
        MarketRate = marketRate;
    }

    public Guid FromCurrencyId { get; set; }
    public Currency? FromCurrency { get; set; }
    public Guid ToCurrencyId { get; set; }
    public Currency? ToCurrency { get; set; }
    public DateTime EffectiveDate { get; set; }
    public decimal MarketRate { get; set; }
}