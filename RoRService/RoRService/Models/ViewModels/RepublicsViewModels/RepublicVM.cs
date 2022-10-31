using RoRService.Models.DataModels;
using RoRService.Models.DataModels.Cards;
using RoRService.Models.ViewModels.CardsViewModels;
using System.Collections.Generic;

namespace RoRService.Models.ViewModels.RepublicsViewModels
{
    public class RepublicVM
    {
        public long GameId { get; set; }
        public int UnrestLevel { get; set; }
        public int StateTreasury { get; set; }
        public List<OfficeVM> Offices { get; set; }
        public List<EventVM> Events { get; set; }
        public List<CardVM> Forum { get; set; }
        public List<CardVM> Wars { get; set; }
        public string LandBills { get; set; }
        public int Fleets { get; set; }
        public List<LegionVM> Legions { get; set; }
        public string Laws { get; set; }
        public List<CardVM> Deck { get; set; }
    }
}