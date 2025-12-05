namespace Domain;

public abstract class BaseEntity
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string CreatedBy { get; set; } = Helpers.AppName;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public string UpdatedBy { get; set; } = Helpers.AppName;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}