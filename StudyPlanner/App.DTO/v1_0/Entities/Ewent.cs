using System.ComponentModel.DataAnnotations;
using Base.Domain;

namespace App.DTO.v1_0.Entities;

public class Ewent : BaseEntityId
{
    [MaxLength(128)]
    public string Label { get; set; } = "No title";
    public string? Description { get; set; }
    public DateTime From { get; set; }
    public DateTime Until { get; set; }

    public Guid SubjectId { get; set; }
    public Subject? Subject { get; set; }
}