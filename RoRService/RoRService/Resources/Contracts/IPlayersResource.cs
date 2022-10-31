using RoRService.Models.DataModels;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace RoRService.Resources.Contracts
{
    public interface IPlayersResource
    {
        Task<HttpResponseMessage> AddPlayerToGame(long gameId, Player newPlayer);
        Task<List<Player>> GetPlayers(long gameId);
    }
}
