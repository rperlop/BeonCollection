namespace BeonCollection.Api.Models;

public class Game
{
    public int Id { get; set; }
    public string Title { get; set; } = "";
    public List<Edition> Editions { get; set; } = new();
}