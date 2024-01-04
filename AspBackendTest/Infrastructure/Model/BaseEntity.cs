namespace AspBackendTest.Infrastructure.Model;

public class BaseEntity
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public DateTime InsertTime { get; set; } = DateTime.Now;
    public DateTime UpdateTime { get; set; } = DateTime.Now;
    public bool IsDeleted { get; set; } = false;
}