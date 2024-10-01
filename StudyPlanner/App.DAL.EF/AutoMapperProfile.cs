using AutoMapper;

namespace App.DAL.EF;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<Domain.DbEntities.Module, DTO.Entities.Module>().ReverseMap();
        CreateMap<Domain.DbEntities.Subject, DTO.Entities.Subject>().ReverseMap();
        CreateMap<Domain.DbEntities.WorkTask, DTO.Entities.WorkTask>().ReverseMap();
        CreateMap<Domain.DbEntities.Curriculum, DTO.Entities.Curriculum>().ReverseMap();
        CreateMap<Domain.DbEntities.Ewent, DTO.Entities.Ewent>().ReverseMap();
        CreateMap<Domain.DbEntities.TimeWindow, DTO.Entities.TimeWindow>().ReverseMap();
        CreateMap<Domain.ManyToMany.EwentRole, DTO.ManyToMany.EwentRole>().ReverseMap();
        CreateMap<Domain.ManyToMany.SubjectRole, DTO.ManyToMany.SubjectRole>().ReverseMap();
        CreateMap<Domain.ManyToMany.UserCurriculum, DTO.ManyToMany.UserCurriculum>().ReverseMap();
        CreateMap<Domain.ManyToMany.UserEwent, DTO.ManyToMany.UserEwent>().ReverseMap();
        CreateMap<Domain.ManyToMany.UserField, DTO.ManyToMany.UserField>().ReverseMap();
        CreateMap<Domain.ManyToMany.UserSubject, DTO.ManyToMany.UserSubject>().ReverseMap();
        CreateMap<Domain.ManyToMany.UserWorkTask, DTO.ManyToMany.UserWorkTask>().ReverseMap();
        CreateMap<Domain.ManyToMany.WorkTaskRole, DTO.ManyToMany.WorkTaskRole>().ReverseMap();
        CreateMap<Domain.ManyToMany.WorkTaskTimeWindow, DTO.ManyToMany.WorkTaskTimeWindow>().ReverseMap();
        CreateMap<Domain.Identity.Role, DTO.Identity.Role>().ReverseMap();
    }
}