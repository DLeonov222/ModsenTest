using ModsenTest.Models;

namespace ModsenTest.Dtos
{
    public class CreateBookDto
    {
        public string ISBN { get; set; }
        public string Title { get; set; }
        public string Genre { get; set; }
        public string Description { get; set; }
        public DateTime BookTakenTime { get; set; } = DateTime.Now;
        public DateTime BookReturnTime { get; set; } = DateTime.Now.AddDays(14);
    }
}
