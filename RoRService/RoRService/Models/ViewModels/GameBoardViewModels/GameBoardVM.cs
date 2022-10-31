using RoRService.Models.ViewModels.CardsViewModels;
using RoRService.Models.ViewModels.FactionsViewModels;
using RoRService.Models.ViewModels.RepublicsViewModels;
using System.Collections.Generic;

namespace RoRService.Models.ViewModels.GameBoardViewModels
{
    public class GameBoardVM
    {
        public GameBoardVM()
        {
            DisplayFactions = true;
            DisplayOffices = true;
            DisplayWars = true;
            DisplayForum = true;
            DisplayGameLog = true;

            FactionList = new List<FactionVM>();
        }

        public string Name { get; set; }
        public List<FactionVM> FactionList { get; set; }
        public RepublicVM Republic { get; set; }
        public bool DisplayFactions { get; set; }
        public bool DisplayOffices { get; set; }
        public bool DisplayWars { get; set; }
        public bool DisplayForum { get; set; }
        public bool DisplayGameLog { get; set; }
    }
}