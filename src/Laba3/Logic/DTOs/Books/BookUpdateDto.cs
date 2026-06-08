namespace Logic.DTOs.Books;

public class BookUpdateDto
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public int YearPublished { get; set; }
    public int AuthorId { get; set; }
    public List<int> GenreIds { get; set; } = new();
}
