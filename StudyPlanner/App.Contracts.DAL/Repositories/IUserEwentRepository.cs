using Base.Contracts.DAL;

namespace App.Contracts.DAL.Repositories;

public interface IUserEwentRepository : IEntityRepository<App.DAL.DTO.ManyToMany.UserEwent>
{
    
}