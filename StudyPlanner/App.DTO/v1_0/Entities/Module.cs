using System.ComponentModel.DataAnnotations;
using Base.Domain;

namespace App.DTO.v1_0.Entities;

public class Module : BaseEntityId
{
    [MaxLength(128)]
    public string Label { get; set; } = default!;
    public int EAP { get; set; }
    public Guid CurriculumId { get; set; }
}