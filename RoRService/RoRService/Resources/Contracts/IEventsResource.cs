using RoRService.Models.DataModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RoRService.Resources.Contracts
{
    public interface IEventsResource
    {
        Task<Event> GetEvent(int eventId, int eventLevel);
        Task<List<Event>> GetEventsForGame(long gameId);
    }
}
