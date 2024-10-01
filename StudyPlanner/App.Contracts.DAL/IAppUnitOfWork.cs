using App.Contracts.DAL.Repositories;
using App.Domain.Identity;
using Base.Contracts.DAL;

namespace App.Contracts.DAL;

public interface IAppUnitOfWork : IUnitOfWork
{
    // list your repos here

    ISubjectRepository Subjects { get; }
    IWorkTaskRepository WorkTasks { get; }
    IModuleRepository Modules { get; }
    ICurriculumRepository Curriculums { get; }
    IEwentRepository Ewents { get; }
    ITimeWindowRepository TimeWindows { get; }
    IEwentRoleRepository EwentRoles { get; }
    ISubjectRoleRepository SubjectRoles { get; }
    IUserCurriculumRepository UserCurriculums { get; }
    IUserEwentRepository UserEwents { get; }
    IUserFieldRepository UserFields { get; }
    IUserSubjectRepository UserSubjects { get; }
    IUserWorkTaskRepository UserWorkTasks { get; }
    IWorkTaskRoleRepository WorkTaskRoles { get; }
    IWorkTaskTimeWindowRepository WorkTaskTimeWindows { get; }
    IRoleRepository Roles { get; }
}