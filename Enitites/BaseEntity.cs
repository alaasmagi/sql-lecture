namespace Domain;

public abstract class BaseEntity
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string CreatedBy { get; set; } = "ois v0.1";
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public string UpdatedBy { get; set; } = "ois v0.1";
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}