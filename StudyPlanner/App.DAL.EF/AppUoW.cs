using App.Contracts.DAL;
using App.Contracts.DAL.Repositories;
using App.DAL.EF.Repositories;
using App.Domain.Identity;
using AutoMapper;
using Base.Contracts.DAL;
using Base.DAL.EF;

namespace App.DAL.EF;

public class AppUoW : BaseUnitOfWork<AppDbContext>, IAppUnitOfWork
{
    private readonly IMapper _mapper;
    public AppUoW(AppDbContext dbContext, IMapper mapper) : base(dbContext)
    {
        _mapper = mapper;
    }

    private ISubjectRepository? _subjects;
    public ISubjectRepository Subjects => _subjects ?? new SubjectRepository(UowDbContext, _mapper);
    
    private IWorkTaskRepository? _workTasks;
    public IWorkTaskRepository WorkTasks => _workTasks ?? new WorkTaskRepository(UowDbContext, _mapper);
    
    private IModuleRepository? _modules;
    public IModuleRepository Modules => _modules ?? new ModuleRepository(UowDbContext, _mapper);
    private ICurriculumRepository? _curriculums;
    public ICurriculumRepository Curriculums => _curriculums ?? new CurriculumRepository(UowDbContext, _mapper);
    private IEwentRepository? _ewents;
    public IEwentRepository Ewents => _ewents ?? new EwentRepository(UowDbContext, _mapper);
    private ITimeWindowRepository? _timeWindows;
    public ITimeWindowRepository TimeWindows => _timeWindows ?? new TimeWindowRepository(UowDbContext, _mapper);
    private IEwentRoleRepository? _ewentRoles;
    public IEwentRoleRepository EwentRoles => _ewentRoles ?? new EwentRoleRepository(UowDbContext, _mapper);
    private ISubjectRoleRepository? _subjectRoles;
    public ISubjectRoleRepository SubjectRoles => _subjectRoles ?? new SubjectRoleRepository(UowDbContext, _mapper);
    private IUserCurriculumRepository? _userCurriculums;
    public IUserCurriculumRepository UserCurriculums => _userCurriculums ?? new UserCurriculumRepository(UowDbContext, _mapper);
    private IUserEwentRepository? _userEwents;
    public IUserEwentRepository UserEwents => _userEwents ?? new UserEwentRepository(UowDbContext, _mapper);
    private IUserFieldRepository? _userFields;
    public IUserFieldRepository UserFields => _userFields ?? new UserFieldRepository(UowDbContext, _mapper);
    private IUserSubjectRepository? _userSubjects;
    public IUserSubjectRepository UserSubjects => _userSubjects ?? new UserSubjectRepository(UowDbContext, _mapper);
    private IUserWorkTaskRepository? _userWorkTasks;
    public IUserWorkTaskRepository UserWorkTasks => _userWorkTasks ?? new UserWorkTaskRepository(UowDbContext, _mapper);
    private IWorkTaskRoleRepository? _workTaskRoles;
    public IWorkTaskRoleRepository WorkTaskRoles => _workTaskRoles ?? new WorkTaskRoleRepository(UowDbContext, _mapper);
    private IWorkTaskTimeWindowRepository? _workTaskTimeWindows;
    public IWorkTaskTimeWindowRepository WorkTaskTimeWindows => _workTaskTimeWindows ?? new WorkTaskTimeWindowRepository(UowDbContext, _mapper);
    private IRoleRepository? _roles;
    public IRoleRepository Roles => _roles ?? new RoleRepository(UowDbContext, _mapper);
}