using RoRService.Models.DataModels;
using RoRService.Repositories.Helpers;
using System.Collections.Generic;

namespace RoRService.Repositories.Contracts
{
    public interface IPlayersRepository
    {
        IEnumerable<Player> GetPlayersByGameId(long gameId);
        Player GetPlayer(long id);
        RepositoryActionResult<Player> CreatePlayer(Player newPlayer);
    }
}
