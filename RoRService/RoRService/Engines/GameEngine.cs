using RoRService.Engines.Contracts;
using RoRService.Models.ViewModels.GameBoardViewModels;
using System.Threading.Tasks;

namespace RoRService.Engines
{
    public class GameEngine : IGameEngine
    {
        public GameEngine()
        {

        }

        public async Task StartGame(long gameId)
        {
            var startUpEngine = new GameStartUpEngine();
            await startUpEngine.SetUpNewGame(gameId);
        }

        public async Task<GameBoardVM> ResumeGame(long gameId)
        {
            var resumeEngine = new GameResumeEngine();
            var gameBoardVM = await resumeEngine.ResumeGame(gameId);
            return gameBoardVM;
        }


    }
}