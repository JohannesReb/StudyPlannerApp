using System.ComponentModel.DataAnnotations;
using App.Domain.DbEntities;
using App.Domain.ManyToMany;
using Base.Contracts.Domain;
using Base.Domain;
using Microsoft.AspNetCore.Identity;

namespace App.Domain.Identity;

public class User : IdentityUser<Guid>, IDomainEntityId
{
    [MaxLength(128)]
    public string FirstName { get; set; } = default!;
    [MaxLength(128)]
    public string LastName { get; set; } = default!;
    public decimal CredibilityScore { get; set; } = 1;

    // public decimal Mathematics { get; set; }
    // public decimal Programming { get; set; }
    // public decimal Literature { get; set; }
    
    public List<UserEwent>? UserEwents { get; set; }
    public List<UserWorkTask>? UserWorkTasks { get; set; }
    public List<UserSubject>? UserSubjects { get; set; }
    public List<TimeWindow>? TimeWindows { get; set; }
    public List<UserCurriculum>? UserCurriculums { get; set; }
    public List<UserField>? UserFields { get; set; }

    public ICollection<RefreshToken>? RefreshTokens { get; set; }
    
}