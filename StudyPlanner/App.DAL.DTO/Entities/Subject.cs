using System.ComponentModel.DataAnnotations;
using App.DAL.DTO.ManyToMany;
using Base.Domain;

namespace App.DAL.DTO.Entities;

public class Subject : BaseEntityIdMetadata
{
    [MaxLength(128)]
    public string Label { get; set; } = default!;
    public string? Description { get; set; }
    public int? EAP { get; set; }
    public Guid? ModuleId { get; set; }
    public Module? Module { get; set; }
}