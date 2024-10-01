using App.BLL.Services;
using App.Contracts.BLL;
using App.Contracts.BLL.Services;
using App.Contracts.DAL;
using App.DAL.EF;
using AutoMapper;
using Base.BLL;

namespace App.BLL;

public class AppBLL: BaseBLL, IAppBLL
{
    private readonly IMapper _mapper;
    private readonly IAppUnitOfWork _uow;
    
    public AppBLL(IAppUnitOfWork uoW, IMapper mapper) : base(uoW)
    {
        _mapper = mapper;
        _uow = uoW;
    }

    private ISubjectService? _subjects;
    public ISubjectService Subjects => _subjects ?? new SubjectService(_uow, _uow.Subjects, _mapper);
    
    private IModuleService? _modules;
    public IModuleService Modules => _modules ?? new ModuleService(_uow, _uow.Modules, _mapper);
    
    private IWorkTaskService? _workTasks;
    public IWorkTaskService WorkTasks => _workTasks ?? new WorkTaskService(_uow, _uow.WorkTasks, _mapper);
    
    private ICurriculumService? _curriculums;
    public ICurriculumService Curriculums => _curriculums ?? new CurriculumService(_uow, _uow.Curriculums, _mapper);
    private IEwentRoleService? _ewentRoles;
    public IEwentRoleService EwentRoles => _ewentRoles ?? new EwentRoleService(_uow, _uow.EwentRoles, _mapper);
    private IEwentService? _ewents;
    public IEwentService Ewents => _ewents ?? new EwentService(_uow, _uow.Ewents, _mapper);
    private ISubjectRoleService? _subjectRoles;
    public ISubjectRoleService SubjectRoles => _subjectRoles ?? new SubjectRoleService(_uow, _uow.SubjectRoles, _mapper);
    private ITimeWindowService? _timeWindows;
    public ITimeWindowService TimeWindows => _timeWindows ?? new TimeWindowService(_uow, _uow.TimeWindows, _mapper);
    private IUserCurriculumService? _userCurriculums;
    public IUserCurriculumService UserCurriculums => _userCurriculums ?? new UserCurriculumService(_uow, _uow.UserCurriculums, _mapper);
    private IUserEwentService? _userEwents;
    public IUserEwentService UserEwents => _userEwents ?? new UserEwentService(_uow, _uow.UserEwents, _mapper);
    private IUserFieldService? _userFields;
    public IUserFieldService UserFields => _userFields ?? new UserFieldService(_uow, _uow.UserFields, _mapper);
    private IUserSubjectService? _userSubjects;
    public IUserSubjectService UserSubjects => _userSubjects ?? new UserSubjectService(_uow, _uow.UserSubjects, _mapper);
    private IUserWorkTaskService? _userWorkTasks;
    public IUserWorkTaskService UserWorkTasks => _userWorkTasks ?? new UserWorkTaskService(_uow, _uow.UserWorkTasks, _mapper);
    private IWorkTaskRoleService? _workTaskRoles;
    public IWorkTaskRoleService WorkTaskRoles => _workTaskRoles ?? new WorkTaskRoleService(_uow, _uow.WorkTaskRoles, _mapper);
    private IWorkTaskTimeWindowService? _workTaskTimeWindows;
    public IWorkTaskTimeWindowService WorkTaskTimeWindows => _workTaskTimeWindows ?? new WorkTaskTimeWindowService(_uow, _uow.WorkTaskTimeWindows, _mapper);
    private IRoleService? _roles;
    public IRoleService Roles => _roles ?? new RoleService(_uow, _uow.Roles, _mapper);
}