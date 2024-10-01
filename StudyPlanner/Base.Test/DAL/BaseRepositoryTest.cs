using AutoMapper;
using Base.DAL.EF;
using Base.Test.Domain;
using Microsoft.EntityFrameworkCore;

namespace Base.Test.DAL;

public class BaseRepositoryTest
{
    private readonly TestDbContext _ctx;
    private readonly TestEntityRepository _testEntityRepository;

    public BaseRepositoryTest()
    {
        // set up mock database - inmemory
        var optionsBuilder = new DbContextOptionsBuilder<TestDbContext>();

        // use random guid as db instance id
        optionsBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());
        _ctx = new TestDbContext(optionsBuilder.Options);

        // reset db
        _ctx.Database.EnsureDeleted();
        _ctx.Database.EnsureCreated();

        var config = new MapperConfiguration(cfg => cfg.CreateMap<TestEntity, TestEntity>());
        var mapper = config.CreateMapper();

        _testEntityRepository =
            new TestEntityRepository(
                _ctx,
                new BaseDalDomainMapper<TestEntity, TestEntity>(mapper)
            );
    }


    [Fact]
    public async Task Test1()
    {
        // Add TestEntity to DB
        var added = _testEntityRepository.Add(new TestEntity() { Value = "Test1" });
        await _ctx.SaveChangesAsync();

        var id = added.Id;
        
        // Get all entities on DB
        var data = await _testEntityRepository.GetAllAsync();
        
        // Should be 1 entity in DB
        Assert.Equal(1, data.Count());
        
        // Add second entity
        var added2 = _testEntityRepository.Add(new TestEntity() { Value = "Test2" });
        await _ctx.SaveChangesAsync();

        var id2 = added2.Id;
        
        // Remove invalid ID, should return 0 entires deleted
        Assert.Equal(0, await _testEntityRepository.RemoveAsync(Guid.Empty));
        
        // Remove first entity, should return 1 entry deleted
        Assert.Equal(1, await _testEntityRepository.RemoveAsync(id));
        await _ctx.SaveChangesAsync();
        
        // Get all entities
        data = await _testEntityRepository.GetAllAsync();
        
        // Should only contain 1 entity, first entity was removed
        Assert.Equal(1, data.Count());
        
        // Get second entity from DB
        var res = await _testEntityRepository.FirstOrDefaultAsync(id2);
        
        // Check it is second entity
        Assert.Equal(added2.Value, res!.Value);
    }
}