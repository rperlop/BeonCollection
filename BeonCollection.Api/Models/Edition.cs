namespace BeonCollection.Api.Models;

public class Edition
{
    public int Id { get; set; }

    public int GameId { get; set; }
    public Game Game { get; set; } = null!;

    public Platform Platform { get; set; }
    public Region Region { get; set; }
    public EditionType Type { get; set; }

    public int? Year { get; set; }
    public List<Copy> Copies { get; set; } = new();
}