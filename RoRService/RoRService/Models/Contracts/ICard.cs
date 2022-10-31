using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using RoRService.Models.DataModels.Enums;

namespace RoRService.Models.Contracts
{
    public interface ICard
    {
        int CardNo { get; set; }
        string Name { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        CardType Type { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        CardStatus Status { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        Era Era { get; set; }

        string Image { get; set; }
        string Text { get; set; }

    }
}
