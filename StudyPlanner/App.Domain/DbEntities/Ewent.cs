using System.ComponentModel.DataAnnotations;
using App.Domain.Identity;
using App.Domain.ManyToMany;
using Base.Domain;

namespace App.Domain.DbEntities;

public class Ewent : BaseEntityIdMetadata
{
    [MaxLength(128)]
    public string Label { get; set; } = "No title";
    public string? Description { get; set; }
    public DateTime From { get; set; }
    public DateTime Until { get; set; }

    public Guid SubjectId { get; set; }
    public Subject? Subject { get; set; }
    
    public List<UserEwent>? UserEwents { get; set; }
    public List<EwentRole>? EwentRoles { get; set; }
}