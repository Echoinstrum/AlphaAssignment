using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Dtos;

public class ProjectDto
{
    public string Id { get; set; } = null!;
    public string ProjectName { get; set; } = null!;
    public string ClientName { get; set; } = null!;
    public string Description { get; set; } = null!;
    public  DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public decimal Budget { get; set; }
    public string Status { get; set; } = null!;
}
