using Logic.DTOs.Books;

namespace Logic.Interfaces;

public interface IBookService
{
    Task<IEnumerable<BookResponseDto>> GetAllAsync();
    Task<BookResponseDto?> GetByIdAsync(int id);
    Task<BookResponseDto> CreateAsync(BookCreateDto dto);
    Task<BookResponseDto?> UpdateAsync(int id, BookUpdateDto dto);
    Task<bool> DeleteAsync(int id);
}
