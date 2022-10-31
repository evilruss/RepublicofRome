using RoRService.Models.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoRService.Repositories.Contracts
{
    public interface IEventsRepository
    {
        Event GetEvent(int eventNo, int eventLevel);
        List<Event> GetEventsForGame(int gameId);
    }
}
