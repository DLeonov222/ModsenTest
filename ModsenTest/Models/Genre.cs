namespace ModsenTest.Models
{
    public class Genre
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string GenreName { get; set; }
        public ICollection<Book> Books { get; set; }
    }
}
