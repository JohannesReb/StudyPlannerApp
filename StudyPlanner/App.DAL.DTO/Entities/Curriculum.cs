using System.ComponentModel.DataAnnotations;
using App.DAL.DTO.ManyToMany;
using App.Domain;
using Base.Domain;

namespace App.DAL.DTO.Entities;

public class Curriculum : BaseEntityId
{
    [MaxLength(128)]
    public string Code { get; set; } = default!;
    [MaxLength(128)]
    public string Label { get; set; } = default!;
    [MaxLength(128)]
    public string Manager { get; set; } = default!;
    public DateTime From { get; set; }
    public DateTime? Until { get; set; }

    public List<Module>? Modules { get; set; }
    public List<UserCurriculum>? UserCurriculums { get; set; }
    public int Semesters { get; set; }
}