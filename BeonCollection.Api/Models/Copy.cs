namespace BeonCollection.Api.Models;

public class Copy
{
    public int Id { get; set; }

    public int EditionId { get; set; }
    public Edition Edition { get; set; } = null!;
    public int OwnerId { get; set; }
    public User Owner { get; set; } = null!; 

    public Contents Contents { get; set; }
    public Condition Condition { get; set; }
    public string? Notes { get; set; }
}