namespace BinasLibraryNowAPI.Modals
{
    public class Book
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Author { get; set; }
        public string? Genre { get; set; }
        public bool? isAvailable { get; set; }
        public int PublishedYear { get; set; }
    }
}