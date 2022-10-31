using RoRService.Models.DataModels.Cards;
using RoRService.Models.DataModels.Enums;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RoRService.Resources.Contracts
{
    public interface ICardsResource
    {
        Task<List<Card>> GetNewDeck(Era gameEra);
        Task<List<Card>> GetDeckForGame(long gameId);
        Task<List<Card>> CreateDeck(long gameId, List<Card> cards);
        Task<Card> UpdateCard(long gameId, Card card);
    }
}
