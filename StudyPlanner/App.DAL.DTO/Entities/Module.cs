﻿using System.ComponentModel.DataAnnotations;
using Base.Domain;

namespace App.DAL.DTO.Entities;

public class Module : BaseEntityId
{
    [MaxLength(128)]
    public string Label { get; set; } = default!;
    public int EAP { get; set; }
    public Guid CurriculumId { get; set; }
    public Curriculum? Curriculum { get; set; }

}