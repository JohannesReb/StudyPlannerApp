using AutoMapper;
using Base.BLL;
using Base.DAL.EF;
using Base.Test.DAL;
using Base.Test.Domain;
using Microsoft.EntityFrameworkCore;

namespace Base.Test.BLL;

public class BaseServiceTest
{
    private readonly TestDbContext _ctx;
    private readonly TestEntityService _testEntityService;
    
    public BaseServiceTest()
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

        var uow = new TestUoW(_ctx);
        var repository = new TestEntityRepository(_ctx, new BaseDalDomainMapper<TestEntity, TestEntity>(mapper));
        
        _testEntityService = new TestEntityService(
            uow,
            repository,
            new BaseBllDalMapper<TestEntity, TestEntity>(mapper));
    }

    [Fact]
    public async Task Test1()
    {
        // Add TestEntity to DB
        var added = _testEntityService.Add(new TestEntity() { Value = "TestValue" });
        await _ctx.SaveChangesAsync();

        var id = added.Id;

        // Get all entities on DB
        var data = await _testEntityService.GetAllAsync();
        
        //Should be 1 entity in DB
        Assert.Equal(1, data.Count());
        
        // Add second entity
        var added2 = _testEntityService.Add(new TestEntity() { Value = "Test2" });
        await _ctx.SaveChangesAsync();

        var id2 = added2.Id;
        
        // Removing invalid ID, should return 0 entiries deleted
        Assert.Equal(0, await _testEntityService.RemoveAsync(Guid.Empty));
        
        // Remove first entity, should return 1 entry deleted
        Assert.Equal(1, await _testEntityService.RemoveAsync(id));
        await _ctx.SaveChangesAsync();
        
        // Get all entities
        data = await _testEntityService.GetAllAsync();
        
        // Should have only 1 entity, because first entity was removed
        Assert.Equal(1, data.Count());

        // Get second entity from DB
        var res = await _testEntityService.FirstOrDefaultAsync(id2);

        // Check it is second entity
        Assert.Equal(added2.Value, res!.Value);
    }
}