using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RoRService.Models.DataModels.Cards
{
    public class CardVariables : Card
    {
        public int Influence { get; set; }
        public int Popularity { get; set; }
        public int Knights { get; set; }
        public bool PriorConsul { get; set; }
        public int Treasury { get; set; }
    }
}