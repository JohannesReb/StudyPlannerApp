using AutoMapper;

namespace WebApp.Helpers;

public class AutoMapperProfile: Profile
{
    public AutoMapperProfile()
    {
        CreateMap<App.BLL.DTO.Entities.Module, App.DTO.v1_0.Entities.Module>().ReverseMap();
        CreateMap<App.BLL.DTO.Entities.Subject, App.DTO.v1_0.Entities.Subject>().ReverseMap();
        CreateMap<App.BLL.DTO.Entities.WorkTask, App.DTO.v1_0.Entities.WorkTask>().ReverseMap();
        CreateMap<App.BLL.DTO.Entities.Curriculum, App.DTO.v1_0.Entities.Curriculum>().ReverseMap();
        CreateMap<App.BLL.DTO.Entities.Ewent, App.DTO.v1_0.Entities.Ewent>().ReverseMap();
        CreateMap<App.BLL.DTO.Entities.TimeWindow, App.DTO.v1_0.Entities.TimeWindow>().ReverseMap();
        CreateMap<App.BLL.DTO.ManyToMany.EwentRole, App.DTO.v1_0.ManyToMany.EwentRole>().ReverseMap();
        CreateMap<App.BLL.DTO.ManyToMany.SubjectRole, App.DTO.v1_0.ManyToMany.SubjectRole>().ReverseMap();
        CreateMap<App.BLL.DTO.ManyToMany.UserCurriculum, App.DTO.v1_0.ManyToMany.UserCurriculum>().ReverseMap();
        CreateMap<App.BLL.DTO.ManyToMany.UserEwent, App.DTO.v1_0.ManyToMany.UserEwent>().ReverseMap();
        CreateMap<App.BLL.DTO.ManyToMany.UserField, App.DTO.v1_0.ManyToMany.UserField>().ReverseMap();
        CreateMap<App.BLL.DTO.ManyToMany.UserSubject, App.DTO.v1_0.ManyToMany.UserSubject>().ReverseMap();
        CreateMap<App.BLL.DTO.ManyToMany.UserWorkTask, App.DTO.v1_0.ManyToMany.UserWorkTask>().ReverseMap();
        CreateMap<App.BLL.DTO.ManyToMany.WorkTaskRole, App.DTO.v1_0.ManyToMany.WorkTaskRole>().ReverseMap();
        CreateMap<App.BLL.DTO.ManyToMany.WorkTaskTimeWindow, App.DTO.v1_0.ManyToMany.WorkTaskTimeWindow>().ReverseMap();
        CreateMap<App.BLL.DTO.Identity.Role, App.DTO.v1_0.Identity.Role>().ReverseMap();
    }
}