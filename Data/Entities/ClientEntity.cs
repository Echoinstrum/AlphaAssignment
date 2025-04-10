using System.ComponentModel.DataAnnotations;

namespace Data.Entities;

public class ClientEntity
{
    [Key]
    public string Id { get; set; } = null!;
    public string ClientName { get; set; } = null!;
}
