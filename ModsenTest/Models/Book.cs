namespace ModsenTest.Models
{
    public class Book
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string ISBN { get; set; }
        public string Title { get; set; }
        public Genre Genre { get; set; }
        public Guid GenreId { get; set; }
        public Author Author { get; set; }
        public Guid AuthorId { get; set; }
        public string Description { get; set; }
        public DateTime BookTakenTime { get; set; }
        public DateTime BookReturnTime { get; set; }
    }
}
