using System.ComponentModel.DataAnnotations;


namespace Agap2It.Labs.Launchpad.Data.Base;

public class Entity
{
    [Key]
    public int Id { get; set; }
}