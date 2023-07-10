namespace ModsenTest.Dtos
{
    public class BookViewDto
    {
        public Guid Id { get; set; }
        public string ISBN { get; set; }
        public string Title { get; set; }
        public string Genre { get; set; }
        public string Description { get; set; }
        public string AuthorName { get; set; }
        public DateTime BookTakenTime { get; set; }
        public DateTime BookReturnTime { get; set; }
    }
}
