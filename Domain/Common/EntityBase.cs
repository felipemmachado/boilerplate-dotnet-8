namespace Domain.Common;
public abstract class EntityBase
{
    public Guid Id { get; set; }
    public Guid? CreatedBy { get; set; }
    public DateTime CreatedAt { get; set; }
    public Guid? UpdatedBy { get; set; }
    public DateTime? UpdatedAt { get; set; }

    public EntityBase()
    {

        Id = Guid.NewGuid();
        UpdatedAt = null;
        UpdatedBy = null;
    }
}
