using Logic.DTOs.Books;
using Logic.Interfaces;
using Microsoft.EntityFrameworkCore;
using Storage.Data;
using Storage.Entities;

namespace Logic.Services;

public class BookService : IBookService
{
    private readonly AppDbContext _context;

    public BookService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<BookResponseDto>> GetAllAsync()
    {
        var books = await _context.Books
            .AsNoTracking()
            .Include(b => b.Author)
            .Include(b => b.Genres)
            .ToListAsync();

        return books.Select(b => new BookResponseDto
        {
            Id = b.Id,
            Title = b.Title,
            YearPublished = b.YearPublished,
            Author = new AuthorInBookDto
            {
                Id = b.Author.Id,
                Name = b.Author.Name
            },
            Genres = b.Genres.Select(g => new GenreInBookDto
            {
                Id = g.Id,
                Name = g.Name
            }).ToList()
        });
    }

    public async Task<BookResponseDto?> GetByIdAsync(int id)
    {
        var book = await _context.Books
            .AsNoTracking()
            .Include(b => b.Author)
            .Include(b => b.Genres)
            .FirstOrDefaultAsync(b => b.Id == id);

        if (book == null) return null;

        return new BookResponseDto
        {
            Id = book.Id,
            Title = book.Title,
            YearPublished = book.YearPublished,
            Author = new AuthorInBookDto
            {
                Id = book.Author.Id,
                Name = book.Author.Name
            },
            Genres = book.Genres.Select(g => new GenreInBookDto
            {
                Id = g.Id,
                Name = g.Name
            }).ToList()
        };
    }

    public async Task<BookResponseDto> CreateAsync(BookCreateDto dto)
    {
        var book = new Book
        {
            Title = dto.Title,
            YearPublished = dto.YearPublished,
            AuthorId = dto.AuthorId
        };

        foreach (var genreId in dto.GenreIds)
        {
            var genre = await _context.Genres.FindAsync(genreId);
            if (genre != null)
            {
                book.Genres.Add(genre);
            }
        }

        _context.Books.Add(book);
        await _context.SaveChangesAsync();

        return await GetByIdAsync(book.Id)
            ?? throw new InvalidOperationException("Book not found after creation");
    }

    public async Task<BookResponseDto?> UpdateAsync(int id, BookUpdateDto dto)
    {
        var book = await _context.Books
            .Include(b => b.Genres)
            .FirstOrDefaultAsync(b => b.Id == id);

        if (book == null) return null;

        book.Title = dto.Title;
        book.YearPublished = dto.YearPublished;
        book.AuthorId = dto.AuthorId;

        book.Genres.Clear();
        foreach (var genreId in dto.GenreIds)
        {
            var genre = await _context.Genres.FindAsync(genreId);
            if (genre != null)
            {
                book.Genres.Add(genre);
            }
        }

        await _context.SaveChangesAsync();
        return await GetByIdAsync(book.Id);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var book = await _context.Books.FindAsync(id);
        if (book == null) return false;

        _context.Books.Remove(book);
        await _context.SaveChangesAsync();
        return true;
    }
}
