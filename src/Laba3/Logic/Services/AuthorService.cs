using Logic.DTOs.Authors;
using Logic.Interfaces;
using Microsoft.EntityFrameworkCore;
using Storage.Data;
using Storage.Entities;

namespace Logic.Services;

public class AuthorService : IAuthorService
{
    private readonly AppDbContext _context;

    public AuthorService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<AuthorResponseDto>> GetAllAsync()
    {
        var authors = await _context.Authors
            .AsNoTracking()
            .ToListAsync();

        return authors.Select(a => new AuthorResponseDto
        {
            Id = a.Id,
            Name = a.Name,
            BirthYear = a.BirthYear
        });
    }

    public async Task<AuthorResponseDto?> GetByIdAsync(int id)
    {
        var author = await _context.Authors
            .AsNoTracking()
            .FirstOrDefaultAsync(a => a.Id == id);

        if (author == null) return null;

        return new AuthorResponseDto
        {
            Id = author.Id,
            Name = author.Name,
            BirthYear = author.BirthYear
        };
    }

    public async Task<AuthorResponseDto> CreateAsync(AuthorCreateDto dto)
    {
        var author = new Author
        {
            Name = dto.Name,
            BirthYear = dto.BirthYear
        };

        _context.Authors.Add(author);
        await _context.SaveChangesAsync();

        return new AuthorResponseDto
        {
            Id = author.Id,
            Name = author.Name,
            BirthYear = author.BirthYear
        };
    }

    public async Task<AuthorResponseDto?> UpdateAsync(int id, AuthorUpdateDto dto)
    {
        var author = await _context.Authors.FindAsync(id);
        if (author == null) return null;

        author.Name = dto.Name;
        author.BirthYear = dto.BirthYear;

        await _context.SaveChangesAsync();

        return new AuthorResponseDto
        {
            Id = author.Id,
            Name = author.Name,
            BirthYear = author.BirthYear
        };
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var author = await _context.Authors.FindAsync(id);
        if (author == null) return false;

        _context.Authors.Remove(author);
        await _context.SaveChangesAsync();
        return true;
    }
}
