using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoRService.Engines.Contracts
{
    public interface IGameStartUpEngine
    {
        Task SetUpNewGame(long gameId);
    }
}
