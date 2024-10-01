using App.Domain.DbEntities;
using App.Domain.Identity;
using App.Domain.ManyToMany;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace App.DAL.EF;

public class AppDbContext : IdentityDbContext<User, Role, Guid, IdentityUserClaim<Guid>, UserRole,
    IdentityUserLogin<Guid>, IdentityRoleClaim<Guid>, IdentityUserToken<Guid>>

{
    public DbSet<Curriculum> Curriculums { get; set; } = default!;
    public DbSet<Ewent> Ewents { get; set; } = default!;
    public DbSet<Module> Modules { get; set; } = default!;
    public DbSet<Subject> Subjects { get; set; } = default!;
    public DbSet<TimeWindow> TimeWindows { get; set; } = default!;
    public DbSet<WorkTask> WorkTasks { get; set; } = default!;
    
    
    public DbSet<EwentRole> EwentRoles { get; set; } = default!;
    public DbSet<SubjectRole> SubjectRoles { get; set; } = default!;
    public DbSet<UserCurriculum> UserCurriculums { get; set; } = default!;
    public DbSet<UserEwent> UserEwents { get; set; } = default!;
    public DbSet<UserField> UserFields { get; set; } = default!;
    public DbSet<UserSubject> UserSubjects { get; set; } = default!;
    public DbSet<UserWorkTask> UserWorkTasks { get; set; } = default!;
    public DbSet<WorkTaskRole> WorkTaskRoles { get; set; } = default!;
    public DbSet<WorkTaskTimeWindow> WorkTaskTimeWindows { get; set; } = default!;
    public DbSet<RefreshToken> RefreshTokens { get; set; } = default!;
    
    public AppDbContext(DbContextOptions options) : base(options)
    {
    }


    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        foreach (var entity in ChangeTracker.Entries().Where(e => e.State != EntityState.Deleted))
        {
            foreach (var prop in entity
                         .Properties
                         .Where(x => x.Metadata.ClrType == typeof(DateTime) || x.Metadata.ClrType == typeof(DateTime?)))
            {
                if (prop.CurrentValue != null)
                {
                    prop.CurrentValue = ((DateTime)prop.CurrentValue).ToUniversalTime();
                    DateTime.SpecifyKind((DateTime)prop.CurrentValue, DateTimeKind.Utc);
                }
            }
        }

        return base.SaveChangesAsync(cancellationToken);
    }
}