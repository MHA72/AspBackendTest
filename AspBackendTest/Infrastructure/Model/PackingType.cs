namespace AspBackendTest.Infrastructure.Model;

public class PackingType : BaseEntity
{
    public PackingType(string name, decimal price)
    {
        Name = name;
        Price = price;
    }

    public string Name { get; set; }
    public decimal Price { get; set; }   
}