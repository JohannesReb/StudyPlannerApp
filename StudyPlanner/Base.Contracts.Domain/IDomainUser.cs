using Microsoft.AspNetCore.Identity;

namespace Base.Contracts.Domain;

public interface IDomainUser<TUser> : IDomainUser<Guid, TUser>, IDomainEntityId
    where TUser : IdentityUser<Guid>
{
}

public interface IDomainUser<TKey, TUser> : IDomainEntityId<TKey>
    where TKey : IEquatable<TKey>
    where TUser : IdentityUser<TKey>
{
    public TUser? User { get; set; }
}