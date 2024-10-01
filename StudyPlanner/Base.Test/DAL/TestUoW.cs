using Base.DAL.EF;

namespace Base.Test.DAL;

public class TestUoW : BaseUnitOfWork<TestDbContext>
{
    public TestUoW(TestDbContext dbContext) : base(dbContext)
    {
    }
}