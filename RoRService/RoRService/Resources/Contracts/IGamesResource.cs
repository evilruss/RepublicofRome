using RoRService.Models.DataModels;
using RoRService.Models.ViewModels.GamesViewModels;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace RoRService.Resources.Contracts
{
    public interface IGamesResource
    {
        Task<List<Game>> GetGameList();
        Task<Game> GetGameDetails(long id);
        Task<HttpResponseMessage> CreateNewGame(Game newGame);
        Task<HttpResponseMessage> UpdateGame(Game game);
    }
}
