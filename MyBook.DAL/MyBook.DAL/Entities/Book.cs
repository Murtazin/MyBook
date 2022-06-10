namespace MyBook.Entity;

public class Book
{
    public Guid Id { get; set; }
    public string Title { get; set; } = null!;
    public Author Author { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string Genre { get; set; } = null!;
    public int SubType { get; set; } //
    public string Image { get; set; } = null!; //Нужно ли стандартный URL для профиля? 
    public int Year { get; set; } //

    public double Rating { get; set; }
    
    public DateTime AddedDate { get; set; }
    public List<User> Users { get; set; } = null!;
}