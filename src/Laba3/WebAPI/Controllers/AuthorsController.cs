using Logic.DTOs.Authors;
using Logic.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class AuthorsController : ControllerBase
{
    private readonly IAuthorService _service;

    public AuthorsController(IAuthorService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<AuthorResponseDto>>> GetAll()
    {
        var authors = await _service.GetAllAsync();
        return Ok(authors);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<AuthorResponseDto>> GetById(int id)
    {
        var author = await _service.GetByIdAsync(id);
        if (author == null)
            return NotFound();

        return Ok(author);
    }

    [HttpPost]
    public async Task<ActionResult<AuthorResponseDto>> Create([FromBody] AuthorCreateDto dto)
    {
        var created = await _service.CreateAsync(dto);
        return CreatedAtAction(
            nameof(GetById),
            new { id = created.Id },
            created);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<AuthorResponseDto>> Update(int id, [FromBody] AuthorUpdateDto dto)
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
