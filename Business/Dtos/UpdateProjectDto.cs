﻿using Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Dtos;

public class UpdateProjectDto
{
    public string ProjectName { get; set; } = null!;
    public string? Image { get; set; }
    public string Description { get; set; } = null!;
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public decimal Budget { get; set; }
    public ProjectEntity.ProjectStatus Status { get; set; }
}
