using RoRService.Models.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace RoRService.Resources.Contracts
{
    public interface IFactionsResource
    {
        Task<List<Faction>> GetFactions(long gameId);
        Task<Faction> GetFaction(long gameId, long factionId);
        Task<HttpResponseMessage> CreateFactions(long gameId, List<Faction> factionList);
    }
}
