using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using RoRService.Models.DataModels.Enums;
using System.Collections.Generic;

namespace RoRService.Models.DataModels.Cards
{
    public class War : Card
    {
        public War()
        {
            SpecialRules = new List<string>();
        }

        public string WarName { get; set; }
        public int Legions { get; set; }
        public int FleetSupport { get; set; }
        public int Fleets { get; set; }
        public int Talents { get; set; }
        public string Disasters { get; set; }
        public string StandOffs { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public CardStatus DefaultStatus { get; set; }
        public List<string> SpecialRules { get; set; }
    }
}