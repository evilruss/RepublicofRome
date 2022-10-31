using RoRService.Models.DataModels.Cards;

namespace RoRService.Models.DataModels
{
    public class Event : Card
    {
        public int No { get; set; }
        public int Level { get; set; }
        public int MaxLevel { get; set; }
    }
}