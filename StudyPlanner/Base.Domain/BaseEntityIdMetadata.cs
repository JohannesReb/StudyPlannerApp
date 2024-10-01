using Base.Contracts.Domain;

namespace Base.Domain;

public abstract class BaseEntityIdMetadata : BaseEntityIdMetadata<Guid>
{
    
}

public abstract class BaseEntityIdMetadata<TKey> : IDomainEntityIdMetadata<TKey>
    where TKey : IEquatable<TKey>
{
    public Guid Id { get; set; }
    public TKey CreatedBy { get; set; } = default!;
    public DateTime CreatedAt { get; set; }

    public TKey UpdatedBy { get; set; } = default!;
    public DateTime UpdatedAt { get; set; }
}