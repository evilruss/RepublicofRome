using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using RoRService.Models.Contracts;
using RoRService.Models.DataModels.Enums;


namespace RoRService.Models.DataModels.Cards
{
    public class Card : ICard
    {
        public long Id { get; set; }
        public long GameId { get; set; }
        public int CardNo { get; set; }
        public string Name { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public CardType Type { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public CardStatus Status { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public Era Era { get; set; }

        public string Image { get; set; }
        public string Text { get; set; }

    }
}