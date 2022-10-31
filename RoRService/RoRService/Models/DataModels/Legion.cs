using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using RoRService.Models.DataModels.Cards;
using RoRService.Models.DataModels.Enums;

namespace RoRService.Models.DataModels
{
    public class Legion
    {
        public int No { get; set; }
        public long GameId { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public LegionStatus Status { get; set; }
        public int Owner { get; set; }
    }
}