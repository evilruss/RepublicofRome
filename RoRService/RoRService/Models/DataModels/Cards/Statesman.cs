using RoRService.Models.Contracts;
using System.Collections.Generic;

namespace RoRService.Models.DataModels.Cards
{
    public class Statesman : Card, IStatesman
    {
        public int FamilyNo { get; set; }
        public string FamilyLetter { get; set; }
        public int Military { get; set; }
        public int Oratory { get; set; }
        public int Loyalty { get; set; }
        public int Influence { get; set; }
        public int Popularity { get; set; }
        public int Knights { get; set; }
        public bool PriorConsul { get; set; }
        public int Treasury { get; set; }
        public List<Card> OwnedCards { get; set; }
        public Office Office { get; set; }
        public List<string> SpecialRules { get; set; }

        public int Votes()
        {
            return Oratory + Knights;
        }
    }
}