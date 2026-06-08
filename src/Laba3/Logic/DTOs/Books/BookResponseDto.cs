namespace Logic.DTOs.Books;

public class BookResponseDto
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public int YearPublished { get; set; }

    public AuthorInBookDto Author { get; set; } = null!;
    public List<GenreInBookDto> Genres { get; set; } = new();
}

public class AuthorInBookDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
}

public class GenreInBookDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
}
