namespace Logic.DTOs.Authors;

public class AuthorUpdateDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int BirthYear { get; set; }
}
