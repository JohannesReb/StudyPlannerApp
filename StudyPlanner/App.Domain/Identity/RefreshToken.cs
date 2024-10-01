using Base.Contracts.Domain;
using Base.Domain;

namespace App.Domain.Identity;

public class RefreshToken : BaseRefreshToken, IDomainEntityId
{
    public Guid UserId { get; set; }
    public User? User { get; set; }
}