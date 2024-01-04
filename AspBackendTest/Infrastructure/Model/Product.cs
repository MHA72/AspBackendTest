using AspBackendTest.Infrastructure.Model.Enum;

namespace AspBackendTest.Infrastructure.Model;

public class Product : BaseEntity
{
    public string FilePath { get; set; }
    public decimal TotalPrice { get; set; }
    public decimal PriceCurrency { get; set; }
    public Currency? Currency { get; set; }
    public Guid CurrencyId { get; set; }
    public string Name { get; set; }
    public string Color { get; set; }
    public float? SizeNumber { get; set; }
    public Size? SizeType { get; set; } 
    public PackingType? PackingType { get; set; }
    public Guid PackingTypeId { get; set; }
}