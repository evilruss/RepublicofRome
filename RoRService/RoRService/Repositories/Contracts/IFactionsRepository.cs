using RoRService.Models.DataModels;
using RoRService.Repositories.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoRService.Repositories.Contracts
{
    public interface IFactionsRepository
    {
        IEnumerable<Faction> GetFactions(long GameId);
        Faction GetFaction(long gameId, int factionId);
        RepositoryActionResult<List<Faction>> CreateFactions(List<Faction> newFactionsList);
    }
}
