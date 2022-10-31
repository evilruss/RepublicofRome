using RoRService.Models.DataModels.Cards;
using System.Collections.Generic;

namespace RoRService.Models.DataModels
{
    public class Republic
    {
        public Republic()
        {
            Offices = new List<Office>();
            Events = new List<Event>();
            Forum = new List<Card>();
            Legions = new List<Legion>();
            Wars = new List<Card>();
        }

        public long GameId { get; set; }
        public int UnrestLevel { get; set; }
        public int StateTreasury { get; set; }
        public List<Office> Offices { get; set; }
        public List<Event> Events { get; set; }
        public List<Card> Forum { get; set; }
        public List<Card> Wars { get; set; }
        public string LandBills { get; set; }
        public int Fleets { get; set; }
        public List<Legion> Legions { get; set; }
        public string Laws { get; set; }
        public List<Card> Deck { get; set; }
    }
}