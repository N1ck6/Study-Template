namespace Storage.Entities;

public class Book
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public int YearPublished { get; set; }

    public int AuthorId { get; set; }
    public Author Author { get; set; } = null!;

    public ICollection<Genre> Genres { get; set; } = new List<Genre>();
}
