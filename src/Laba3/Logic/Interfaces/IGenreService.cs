using Logic.DTOs.Genres;

namespace Logic.Interfaces;

public interface IGenreService
{
    Task<IEnumerable<GenreResponseDto>> GetAllAsync();
    Task<GenreResponseDto?> GetByIdAsync(int id);
    Task<GenreResponseDto> CreateAsync(GenreCreateDto dto);
    Task<GenreResponseDto?> UpdateAsync(int id, GenreUpdateDto dto);
    Task<bool> DeleteAsync(int id);
}
