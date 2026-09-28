using BeonCollection.Api.Data;
using BeonCollection.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BeonCollection.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GamesController : ControllerBase
{
    private readonly BeonDbContext _db;

    public GamesController(BeonDbContext db)
    {
        _db = db;
    }

    [HttpGet]
    public async Task<ActionResult<List<Game>>> GetAll()
    {
        return await _db.Games.ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Game>> GetById(int id)
    {
        var game = await _db.Games.FindAsync(id);
        if (game is null) return NotFound();
        return game;
    }

    [HttpPost]
    public async Task<ActionResult<Game>> Create(Game game)
    {
        _db.Games.Add(game);
        await _db.SaveChangesAsync();
        return CreatedAtAction(nameof(GetById), new { id = game.Id }, game);
    }
}