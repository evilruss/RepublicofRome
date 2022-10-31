using RoRService.Models.Contracts;
using System.Collections.Generic;

namespace RoRService.Models.DataModels.Cards
{
    public class Intrigue : Card, IIntrigue
    {
        public List<string> SpecialRules { get; set; }
    }
}