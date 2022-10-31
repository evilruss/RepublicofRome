using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using RoRService.Models.DataModels.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RoRService.Models.ViewModels.CardsViewModels
{
    public class CardVM
    {
        public int CardNo { get; set; }
        public string Name { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public CardType Type { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public CardStatus Status { get; set; }

        public string Image { get; set; }
        public string Text { get; set; }
    }
}