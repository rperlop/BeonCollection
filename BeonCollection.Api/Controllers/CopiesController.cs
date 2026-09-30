using BeonCollection.Api.Data;
using BeonCollection.Api.Dtos;
using BeonCollection.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace BeonCollection.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class CopiesController : ControllerBase
{
    private readonly BeonDbContext _db;

    public CopiesController(BeonDbContext db)
    {
        _db = db;
    }

    [AllowAnonymous]
    [HttpGet]
    public async Task<ActionResult<List<CopyDto>>> GetAll([FromQuery] int? editionId)
    {
        var query = _db.Copies.AsQueryable();

        if (editionId.HasValue)
        {
            query = query.Where(c => c.EditionId == editionId.Value);
        }

        return await query
            .Select(c => new CopyDto(c.Id, c.EditionId, c.Contents, c.Condition, c.Notes))
            .ToListAsync();
    }

    [AllowAnonymous]
    [HttpGet("{id}")]
    public async Task<ActionResult<CopyDto>> GetById(int id)
    {
        var copy = await _db.Copies.FindAsync(id);
        if (copy is null) return NotFound();

        return new CopyDto(copy.Id, copy.EditionId, copy.Contents, copy.Condition, copy.Notes);
    }

    [HttpPost]
    public async Task<ActionResult<CopyDto>> Create(CreateCopyDto dto)
    {
        var editionExists = await _db.Editions.AnyAsync(e => e.Id == dto.EditionId);
        if (!editionExists) return BadRequest($"Edition {dto.EditionId} does not exist.");
        var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

        var copy = new Copy
        {
            EditionId = dto.EditionId,
            Contents = dto.Contents,
            Condition = dto.Condition,
            Notes = dto.Notes,
            OwnerId = userId
        };

        _db.Copies.Add(copy);
        await _db.SaveChangesAsync();

        var result = new CopyDto(copy.Id, copy.EditionId, copy.Contents, copy.Condition, copy.Notes);
        return CreatedAtAction(nameof(GetById), new { id = copy.Id }, result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, CreateCopyDto dto)
    {
        var copy = await _db.Copies.FindAsync(id);
        if (copy is null) return NotFound();

        var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
        if (copy.OwnerId != userId) return Forbid();

        var editionExists = await _db.Editions.AnyAsync(e => e.Id == dto.EditionId);
        if (!editionExists) return BadRequest($"Edition {dto.EditionId} does not exist.");

        copy.EditionId = dto.EditionId;
        copy.Contents = dto.Contents;
        copy.Condition = dto.Condition;
        copy.Notes = dto.Notes;

        await _db.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var copy = await _db.Copies.FindAsync(id);
        if (copy is null) return NotFound();

        var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
        if (copy.OwnerId != userId) return Forbid();

        _db.Copies.Remove(copy);
        await _db.SaveChangesAsync();
        return NoContent();
    }
}