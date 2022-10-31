using RoRService.Models.ViewModels.GameBoardViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoRService.Engines.Contracts
{
    public interface IGameEngine
    {
        Task StartGame(long gameId);
        Task<GameBoardVM> ResumeGame(long gameId);
    }
}
