using RoRService.Models.Contracts;
using RoRService.Models.DataModels.Cards;
using RoRService.Models.DataModels.Enums;
using System.Collections.Generic;
using System.Linq;

namespace RoRService.Models.DataModels
{
    public class Faction
    {
        public long Id { get; set; }
        //Each faction has a number to help determine play order.
        public int No { get; set; }
        public long GameId { get; set; }
        public long PlayerId { get; set; }
        public string Name { get; set; }
        public string PlayerName { get; set; }
        public int Treasury { get; set; }
        public int Leader { get; set; }
        public List<Card> Senators { get; set; }
        public List<Card> Hand { get; set; }
        public int Votes
        {
            get
            {
                int votes = 0;
                foreach (var card in Senators.Where(s => s.Status == CardStatus.InPlay && (s.GetType() == typeof(Senator))))
                {
                    var senator = (Senator)card;
                    votes = votes + senator.Votes();
                }
                foreach (var card in Senators.Where(s => s.Status == CardStatus.InPlay && (s.GetType() == typeof(Statesman))))
                {
                    var statesman = (Statesman)card;
                    votes = votes + statesman.Votes();
                }
                return votes;
            }
        }
    }
}