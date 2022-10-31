using RoRService.Models.DataModels.Cards;
using System.Collections.Generic;

namespace RoRService.Models.Contracts
{
    public interface ISenator : ICard
    {
        int FamilyNo { get; set; }
        int Military { get; set; }
        int Oratory { get; set; }
        int Loyalty { get; set; }
        int Influence { get; set; }
        int Popularity { get; set; }
        int Knights { get; set; }
        bool PriorConsul { get; set; }
        int Treasury { get; set; }
        List<Card> OwnedCards { get; set; }
        int Votes();

    }
}
