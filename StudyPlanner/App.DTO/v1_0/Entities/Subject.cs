using System.ComponentModel.DataAnnotations;
using Base.Domain;

namespace App.DTO.v1_0.Entities;

public class Subject : BaseEntityId
{
    [MaxLength(128)]
    public string Label { get; set; } = default!;
    public string? Description { get; set; }
    public int? EAP { get; set; }
    public Guid? ModuleId { get; set; }
    public Module? Module { get; set; }
    public Guid CreatedBy { get; set; }
}