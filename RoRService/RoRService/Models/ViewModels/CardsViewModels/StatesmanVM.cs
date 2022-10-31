using RoRService.Models.DataModels;
using System.Collections.Generic;


namespace RoRService.Models.ViewModels.CardsViewModels
{
    public class StatesmanVM : CardVM
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
        public bool FactionLeader { get; set; }
        public List<CardVM> OwnedCards { get; set; }
        public Office Office { get; set; }
        public string Governor { get; set; }
        public int GovernorTerm { get; set; }
        public int GetVotes()
        {
            return Oratory + Knights;
        }
    }
}