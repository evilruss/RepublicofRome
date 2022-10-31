using RoRService.Models.Contracts;
using RoRService.Models.DataModels;
using RoRService.Models.DataModels.Enums;
using RoRService.Models.ViewModels.PlayersViewModels;
using System;
using System.Collections.Generic;


namespace RoRService.Models.ViewModels.GamesViewModels
{
    public class GameDetailsVM : IGame
    {
        public GameDetailsVM()
        {
            PlayerList = new List<Player>();
        }

        public long Id { get; set; }
        public string Name { get; set; }
        public GameStatus Status { get; set; }
        public string Owner { get; set; }
        public string Description { get; set; }
        public string Era { get; set; }
        public string DateOfLastAction { get; set; }
        public string FormatedDateOfLastAction
        {
            get
            {
                try
                {
                    var extractedDate = DateTime.ParseExact(DateOfLastAction, "yyyyMMddHHmmss", System.Globalization.CultureInfo.InvariantCulture);
                    return extractedDate.ToString("dd/MM/yyyy HH:mm:ss");
                }
                catch
                {
                    return DateOfLastAction;
                };
            }
        }
        public List<Player> PlayerList { get; set; }
        public NewPlayerVM NewPlayer { get; set; }
    }
}