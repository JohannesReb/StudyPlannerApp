using App.Contracts.BLL.Services;
using App.Contracts.DAL.Repositories;
using Base.Contracts.BLL;

namespace App.Contracts.BLL;

public interface IAppBLL : IBLL
{
    ISubjectService Subjects { get; }
    IWorkTaskService WorkTasks { get; }
    IModuleService Modules { get; }
    ICurriculumService Curriculums { get; }
    IEwentRoleService EwentRoles { get; }
    IEwentService Ewents { get; }
    ISubjectRoleService SubjectRoles { get; }
    ITimeWindowService TimeWindows { get; }
    IUserCurriculumService UserCurriculums { get; }
    IUserEwentService UserEwents { get; }
    IUserFieldService UserFields { get; }
    IUserSubjectService UserSubjects { get; }
    IUserWorkTaskService UserWorkTasks { get; }
    IWorkTaskRoleService WorkTaskRoles { get; }
    IWorkTaskTimeWindowService WorkTaskTimeWindows { get; }
    IRoleService Roles { get; }
}