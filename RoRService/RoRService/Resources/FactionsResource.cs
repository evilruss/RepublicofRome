using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RoRService.Helpers;
using RoRService.Models.DataModels;
using RoRService.Models.DataModels.Cards;
using RoRService.Models.DataModels.Enums;
using RoRService.Resources.Contracts;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace RoRService.Resources
{
    public class FactionsResource : IFactionsResource
    {
        public async Task<List<Faction>> GetFactions(long gameId)
        {
            var factionList = new List<Faction>();

            using (var client = HttpClientHelper.GetHttpClient())
            {
                string url = "api/Games/" + gameId + "/Factions";

                HttpResponseMessage result = await client.GetAsync(url);

                if (result.IsSuccessStatusCode)
                {
                    var response = result.Content.ReadAsStringAsync().Result;
                    var jResponse = JArray.Parse(response);

                    foreach (var jFaction in jResponse)
                    {
                        var faction = jFaction.ToObject<Faction>();
                        faction.Senators = new List<Card>();
                        faction.Hand = new List<Card>();

                        var jCardList = jFaction.ToObject<JObject>().GetValue("senators");

                        foreach (var jCard in jCardList)
                        {
                            switch ((CardType)Enum.Parse(typeof(CardType), jCard.ToObject<JObject>().GetValue("type").ToString()))
                            {
                                case CardType.Senator:
                                    faction.Senators.Add(jCard.ToObject<Senator>());
                                    break;
                                case CardType.Statesman:
                                    faction.Senators.Add(jCard.ToObject<Statesman>());
                                    break;
                            }
                        }

                        jCardList = jFaction.ToObject<JObject>().GetValue("hand");

                        foreach (var jCard in jCardList)
                        {
                            switch ((CardType)Enum.Parse(typeof(CardType), jCard.ToObject<JObject>().GetValue("type").ToString()))
                            {
                                case CardType.Senator:
                                    faction.Hand.Add(jCard.ToObject<Senator>());
                                    break;
                                case CardType.Statesman:
                                    faction.Hand.Add(jCard.ToObject<Statesman>());
                                    break;
                                case CardType.Concession:
                                    faction.Hand.Add(jCard.ToObject<Concession>());
                                    break;
                                case CardType.Intrigue:
                                    faction.Hand.Add(jCard.ToObject<Intrigue>());
                                    break;
                            }
                        }

                        factionList.Add(faction);
                    }
                }

                return factionList;
            }
        }

        public async Task<Faction> GetFaction(long gameId, long factionId)
        {
            var faction = new Faction();

            using (var client = HttpClientHelper.GetHttpClient())
            {
                string url = "api/Games/" + gameId + "/Factions" + factionId;

                HttpResponseMessage result = await client.GetAsync(url);

                if (result.IsSuccessStatusCode)
                {
                    var response = result.Content.ReadAsStringAsync().Result;
                    faction = JsonConvert.DeserializeObject<Faction>(response);
                }

                return faction;
            }
        }

        public async Task<HttpResponseMessage> CreateFactions(long gameId, List<Faction> factionList)
        {
            using (var client = HttpClientHelper.GetHttpClient())
            {
                string url = "api/Games/" + gameId + "/Factions";

                HttpResponseMessage result = await client.PostAsJsonAsync(url, factionList);

                return result;
            }
        }
    }
}

