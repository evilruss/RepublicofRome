using RoRService.Models.DataModels.Cards;
using System.Collections.Generic;

namespace RoRService.Models.DataModels
{
    public class GameBoard
    {
        public int Id { get; set; }
        public int GameId { get; set; }
        public List<Faction> FactionList { get; set; }
        public List<Card> Deck { get; set; }
        public int UnrestLevel { get; set; }
        public List<Legion> StateArmy { get; set; }
        public List<Ship> StateNavy { get; set; }
        public string ActiveWars { get; set; }
        public string InactiveWars { get; set; }
        public string LandBills { get; set; }
        public string Forum { get; set; }
        public string Events { get; set; }
        public string Provinces { get; set; }
    }
}