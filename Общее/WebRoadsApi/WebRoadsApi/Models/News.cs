namespace WebRoadsApi.Models
{
    public class News
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public string Date { get; set; }
        public string Picture { get; set; }
        public int PositiveReactions { get; set; }
        public int NegativeReactions { get; set; }
    }
}
