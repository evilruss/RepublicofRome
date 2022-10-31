using RoRService.Models.ViewModels.CardsViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RoRService.Models.ViewModels.FactionsViewModels
{
    public class FactionVM
    {
        public int No { get; set; }
        public string Name { get; set; }
        public string PlayerName { get; set; }
        public int Treasury { get; set; }
        public int Leader { get; set; }
        public List<CardVM> Senators { get; set; }
        public List<CardVM> Hand { get; set; }
        public int Votes { get; set; }
    }
}