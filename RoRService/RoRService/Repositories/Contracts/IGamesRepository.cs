using RoRService.Models.DataModels;
using RoRService.Repositories.Helpers;
using System.Collections.Generic;

namespace RoRService.Repositories.Contracts
{
    public interface IGamesRepository
    {
        IEnumerable<Game> GetGames();
        Game GetGame(long id);
        RepositoryActionResult<Game> CreateGame(Game newGame);
        RepositoryActionResult<Game> UpdateGame(Game newGame);
        void DeleteGame(long id);
    }
}
