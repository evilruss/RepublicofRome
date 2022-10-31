using RoRService.Models.Contracts;
using System.Collections.Generic;

namespace RoRService.Models.DataModels.Cards
{
    public class Concession : Card, IConcession
    {
        public int Income { get; set; }
        public List<string> SpecialRules { get; set; }
    }
}