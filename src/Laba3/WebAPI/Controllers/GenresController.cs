using Logic.DTOs.Genres;
using Logic.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GenresController : ControllerBase
{
    private readonly IGenreService _service;

    public GenresController(IGenreService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<GenreResponseDto>>> GetAll()
    {
        var genres = await _service.GetAllAsync();
        return Ok(genres);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<GenreResponseDto>> GetById(int id)
    {
        var genre = await _service.GetByIdAsync(id);
        if (genre == null)
            return NotFound();

        return Ok(genre);
    }

    [HttpPost]
    public async Task<ActionResult<GenreResponseDto>> Create([FromBody] GenreCreateDto dto)
    {
        var created = await _service.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<GenreResponseDto>> Update(int id, [FromBody] GenreUpdateDto dto)
    {
        if (id != dto.Id)
            return BadRequest("Id mismatch");

        var updated = await _service.UpdateAsync(id, dto);
        if (updated == null)
            return NotFound();

        return Ok(updated);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _service.DeleteAsync(id);
        if (!deleted)
            return NotFound();

        return NoContent();
    }
}
