using Base.Contracts.DAL;
using Base.DAL.EF;
using Base.Test.Domain;
using Microsoft.EntityFrameworkCore;

namespace Base.Test.DAL;

public class TestEntityRepository : BaseEntityRepository<TestEntity, TestEntity, TestDbContext>
{
    public TestEntityRepository(TestDbContext dbContext, IDalMapper<TestEntity, TestEntity> mapper) : base(dbContext, mapper)
    {
    }

    public override async Task<int> RemoveAsync(Guid id, Guid userId = default)
    {
        var entity = await RepoDbSet.FirstOrDefaultAsync(e => e.Id.Equals(id));
        if (entity != null)
        {
            RepoDbSet.Remove(entity);
            return await RepoDbContext.SaveChangesAsync();
        }

        return 0;
    }
}