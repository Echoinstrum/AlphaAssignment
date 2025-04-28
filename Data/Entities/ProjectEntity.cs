using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities;

public class ProjectEntity
{
    [Key]
    public string Id { get; set; } = Guid.NewGuid().ToString();
    [Required]
    public string ProjectName { get; set; } = null!;
    public string? Image {  get; set; }
    [Required]
    public string Description { get; set; } = null!;
    [Required]
    [Column(TypeName = "date")]
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    [Required]
    public decimal Budget { get; set; }
    public DateTime Created { get; set; } = DateTime.Now;
    [Required]
    public ProjectStatus Status { get; set; } = ProjectStatus.Started;

    [ForeignKey(nameof(Client))]
    public string ClientId { get; set; } = null!;
    public ClientEntity Client { get; set; } = null!;


    public enum ProjectStatus
    {
        Started,
        Completed
    }

}
