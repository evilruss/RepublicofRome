using RoRService.Models.Contracts;
using RoRService.Models.DataModels.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace RoRService.Models.ViewModels.GamesViewModels
{
    public class GameListVM : IGame
    {
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
    }
}