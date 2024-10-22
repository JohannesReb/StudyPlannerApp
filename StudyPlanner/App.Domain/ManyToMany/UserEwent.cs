﻿using App.Domain.DbEntities;
using App.Domain.Identity;
using Base.Domain;

namespace App.Domain.ManyToMany;

public class UserEwent : BaseEntityId
{
    public Guid UserId { get; set; }
    public User? User { get; set; }
    public Guid EwentId { get; set; }
    public Ewent? Ewent { get; set; }
}