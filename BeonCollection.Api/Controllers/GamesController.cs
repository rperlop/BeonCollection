using System.Linq.Expressions;
using BeonCollection.Api.Data;
using BeonCollection.Api.Dtos;
using BeonCollection.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace BeonCollection.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class GamesController : ControllerBase
{
    private readonly BeonDbContext _db;

    private static readonly Expression<Func<Game, GameDto>> ToDto = g =>
        new GameDto(
            g.Id,
            g.Title,
            g.Editions
                .Select(e => new EditionDto(e.Id, e.GameId, e.Platform, e.Region, e.Type, e.Year))
                .ToList());

    public GamesController(BeonDbContext db)
    {
        _db = db;
    }

    [AllowAnonymous]
    [HttpGet]
    public async Task<ActionResult<List<GameDto>>> GetAll([FromQuery] Platform? platform)
    {
        var query = _db.Games.AsQueryable();

        if (platform.HasValue)
        {
            query = query.Where(g => g.Editions.Any(e => e.Platform == platform.Value));
        }

        return await query.OrderBy(g => g.Title).Select(ToDto).ToListAsync();
    }

    [AllowAnonymous]
    [HttpGet("{id}")]
    public async Task<ActionResult<GameDto>> GetById(int id)
    {
        var game = await _db.Games.Where(g => g.Id == id).Select(ToDto).FirstOrDefaultAsync();
        if (game is null) return NotFound();
        return game;
    }

    [Authorize]
    [HttpPost]
    public async Task<ActionResult<GameDto>> Create(CreateGameDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.Title))
        {
            return BadRequest("Title is required.");
        }

        var game = new Game { Title = dto.Title.Trim() };
        _db.Games.Add(game);
        await _db.SaveChangesAsync();

        var result = new GameDto(game.Id, game.Title, new List<EditionDto>());
        return CreatedAtAction(nameof(GetById), new { id = game.Id }, result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, CreateGameDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.Title))
        {
            return BadRequest("Title is required.");
        }

        var game = await _db.Games.FindAsync(id);
        if (game is null) return NotFound();

        game.Title = dto.Title.Trim();
        await _db.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var game = await _db.Games.FindAsync(id);
        if (game is null) return NotFound();

        _db.Games.Remove(game);
        await _db.SaveChangesAsync();

        return NoContent();
    }
}