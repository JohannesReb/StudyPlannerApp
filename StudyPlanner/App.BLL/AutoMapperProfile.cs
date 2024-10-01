using AutoMapper;

namespace App.BLL;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<App.DAL.DTO.Entities.Module, DTO.Entities.Module>().ReverseMap();
        CreateMap<App.DAL.DTO.Entities.Subject, DTO.Entities.Subject>().ReverseMap();
        CreateMap<App.DAL.DTO.Entities.WorkTask, DTO.Entities.WorkTask>().ReverseMap();
        CreateMap<App.DAL.DTO.Entities.Curriculum, DTO.Entities.Curriculum>().ReverseMap();
        CreateMap<App.DAL.DTO.Entities.Ewent, DTO.Entities.Ewent>().ReverseMap();
        CreateMap<App.DAL.DTO.Entities.TimeWindow, DTO.Entities.TimeWindow>().ReverseMap();
        CreateMap<App.DAL.DTO.ManyToMany.EwentRole, DTO.ManyToMany.EwentRole>().ReverseMap();
        CreateMap<App.DAL.DTO.ManyToMany.SubjectRole, DTO.ManyToMany.SubjectRole>().ReverseMap();
        CreateMap<App.DAL.DTO.ManyToMany.UserCurriculum, DTO.ManyToMany.UserCurriculum>().ReverseMap();
        CreateMap<App.DAL.DTO.ManyToMany.UserEwent, DTO.ManyToMany.UserEwent>().ReverseMap();
        CreateMap<App.DAL.DTO.ManyToMany.UserField, DTO.ManyToMany.UserField>().ReverseMap();
        CreateMap<App.DAL.DTO.ManyToMany.UserSubject, DTO.ManyToMany.UserSubject>().ReverseMap();
        CreateMap<App.DAL.DTO.ManyToMany.UserWorkTask, DTO.ManyToMany.UserWorkTask>().ReverseMap();
        CreateMap<App.DAL.DTO.ManyToMany.WorkTaskRole, DTO.ManyToMany.WorkTaskRole>().ReverseMap();
        CreateMap<App.DAL.DTO.ManyToMany.WorkTaskTimeWindow, DTO.ManyToMany.WorkTaskTimeWindow>().ReverseMap();
        CreateMap<App.DAL.DTO.Identity.Role, DTO.Identity.Role>().ReverseMap();
    }
}