using RoRService.Models.Contracts;
using RoRService.Models.DataModels;
using RoRService.Models.DataModels.Cards;
using RoRService.Models.DataModels.Enums;
using RoRService.Repositories.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoRService.Repositories.Contracts
{
    public interface ICardsRepository
    {
        List<Card> GetCards();
        List<Card> GetCardsByEra(Era era);
        List<Card> GetAllCardsForGame(long gameId);
        RepositoryActionResult<List<CardVariables>> CreateCardVariables(List<CardVariables> cardList, long gameId);
        RepositoryActionResult<CardVariables> UpdateCardVariables(CardVariables cardVariables);
    }
}
