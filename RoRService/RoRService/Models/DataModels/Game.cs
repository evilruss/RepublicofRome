using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using RoRService.Models.DataModels.Enums;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace RoRService.Models.DataModels
{
    public class Game
    {
        public Game()
        {
            PlayerList = new List<Player>();
        }

        public long Id { get; set; }
        public string Name { get; set; }
        public string Owner { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public GameStatus Status { get; set; }
        public string Description { get; set; }
        public string DateOfLastAction { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public Era Era { get; set; }
        public List<Player> PlayerList { get; set; }
    }
}