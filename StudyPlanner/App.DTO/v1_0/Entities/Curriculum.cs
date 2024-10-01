using System.ComponentModel.DataAnnotations;
using App.Domain;
using Base.Domain;

namespace App.DTO.v1_0.Entities;

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
    public int Semesters { get; set; }
}