namespace RoRService.Models.DataModels
{
    public class Office
    {
        public long OfficeId { get; set; }
        public long GameId { get; set; }
        public int CardNo { get; set; }
        public string Title { get; set; }
        public int Rank { get; set; }
        public int Influence { get; set; }
        public string Name { get; set; }
    }
}