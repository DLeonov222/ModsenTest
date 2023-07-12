namespace ModsenTest.Dtos
{
    public class UpdateBookDto
    {
        public string ISBN { get; set; }
        public string Title { get; set; }
        public string GenreName { get; set; }
        public string Description { get; set; }
        public DateTime BookTakenTime { get; set; }
        public DateTime BookReturnTime { get; set; }
    }
}
