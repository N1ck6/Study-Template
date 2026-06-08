using Logic.DTOs.Genres;
using Logic.Interfaces;
using Microsoft.EntityFrameworkCore;
using Storage.Data;
using Storage.Entities;

namespace Logic.Services;

public class GenreService : IGenreService
{
    private readonly AppDbContext _context;

    public GenreService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<GenreResponseDto>> GetAllAsync()
    {
        return await _context.Genres
            .AsNoTracking()
            .Select(g => new GenreResponseDto
            {
                Id = g.Id,
                Name = g.Name
            })
            .ToListAsync();
    }

    public async Task<GenreResponseDto?> GetByIdAsync(int id)
    {
        var genre = await _context.Genres
            .AsNoTracking()
            .FirstOrDefaultAsync(g => g.Id == id);

        if (genre == null) return null;

        return new GenreResponseDto
        {
            Id = genre.Id,
            Name = genre.Name
        };
    }

    public async Task<GenreResponseDto> CreateAsync(GenreCreateDto dto)
    {
        var genre = new Genre
        {
            Name = dto.Name
        };

        _context.Genres.Add(genre);
        await _context.SaveChangesAsync();

        return new GenreResponseDto
        {
            Id = genre.Id,
            Name = genre.Name
        };
    }

    public async Task<GenreResponseDto?> UpdateAsync(int id, GenreUpdateDto dto)
    {
        var genre = await _context.Genres.FindAsync(id);
        if (genre == null) return null;

        genre.Name = dto.Name;
        await _context.SaveChangesAsync();

        return new GenreResponseDto
        {
            Id = genre.Id,
            Name = genre.Name
        };
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var genre = await _context.Genres.FindAsync(id);
        if (genre == null) return false;

        _context.Genres.Remove(genre);
        await _context.SaveChangesAsync();
        return true;
    }
}
