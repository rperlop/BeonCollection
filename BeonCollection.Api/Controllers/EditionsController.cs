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
public class EditionsController : ControllerBase
{
    private readonly BeonDbContext _db;

    public EditionsController(BeonDbContext db)
    {
        _db = db;
    }

    [AllowAnonymous]
    [HttpGet]
    public async Task<ActionResult<List<EditionDto>>> GetAll()
    {
        return await _db.Editions
            .Select(e => new EditionDto(e.Id, e.GameId, e.Platform, e.Region, e.Type, e.Year))
            .ToListAsync();
    }

    [AllowAnonymous]
    [HttpGet("{id}")]
    public async Task<ActionResult<EditionDto>> GetById(int id)
    {
        var edition = await _db.Editions.FindAsync(id);
        if (edition is null) return NotFound();

        return new EditionDto(edition.Id, edition.GameId, edition.Platform,
            edition.Region, edition.Type, edition.Year);
    }

    [HttpPost]
    public async Task<ActionResult<EditionDto>> Create(CreateEditionDto dto)
    {
        var gameExists = await _db.Games.AnyAsync(g => g.Id == dto.GameId);
        if (!gameExists) return BadRequest($"Game {dto.GameId} does not exist.");

        var edition = new Edition
        {
            GameId = dto.GameId,
            Platform = dto.Platform,
            Region = dto.Region,
            Type = dto.Type,
            Year = dto.Year
        };

        _db.Editions.Add(edition);
        await _db.SaveChangesAsync();

        var result = new EditionDto(edition.Id, edition.GameId, edition.Platform,
            edition.Region, edition.Type, edition.Year);

        return CreatedAtAction(nameof(GetById), new { id = edition.Id }, result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, CreateEditionDto dto)
    {
        var edition = await _db.Editions.FindAsync(id);
        if (edition is null) return NotFound();

        var gameExists = await _db.Games.AnyAsync(g => g.Id == dto.GameId);
        if (!gameExists) return BadRequest($"Game {dto.GameId} does not exist.");

        edition.GameId = dto.GameId;
        edition.Platform = dto.Platform;
        edition.Region = dto.Region;
        edition.Type = dto.Type;
        edition.Year = dto.Year;

        await _db.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var edition = await _db.Editions.FindAsync(id);
        if (edition is null) return NotFound();

        _db.Editions.Remove(edition);
        await _db.SaveChangesAsync();
        return NoContent();
    }
}