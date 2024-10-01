namespace Base.Contracts.Domain;

public interface IDomainEntityIdMetadata<TKey> : IDomainEntityId
{
    public TKey CreatedBy { get; set; }

    public DateTime CreatedAt { get; set; }

    public TKey UpdatedBy { get; set; }
    
    public DateTime UpdatedAt { get; set; }
}