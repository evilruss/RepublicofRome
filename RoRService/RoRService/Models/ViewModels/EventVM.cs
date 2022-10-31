using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RoRService.Models.ViewModels
{
    public class EventVM
    {
        public int No { get; set; }
        public int GameId { get; set; }
        public string Name { get; set; }
        public int Level { get; set; }
        public string Text { get; set; }
        public string Image { get; set; }
    }
}