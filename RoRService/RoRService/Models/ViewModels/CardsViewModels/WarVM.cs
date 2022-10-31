using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Collections.Generic;


namespace RoRService.Models.ViewModels.CardsViewModels
{
    public class WarVM : CardVM
    {
        public WarVM()
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
        public List<string> SpecialRules { get; set; }
    }
}