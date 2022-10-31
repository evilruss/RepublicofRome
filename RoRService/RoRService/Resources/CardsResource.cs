using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RoRService.Helpers;
using RoRService.Models.Contracts;
using RoRService.Models.DataModels.Cards;
using RoRService.Models.DataModels.Enums;
using RoRService.Resources.Contracts;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace RoRService.Resources
{
    public class CardsResource : ICardsResource
    {
        public async Task<List<Card>> GetNewDeck(Era gameEra)
        {
            var deck = new List<Card>();
            var getURL = "api/Cards/" + gameEra.ToString();

            using (var client = HttpClientHelper.GetHttpClient())
            {
                HttpResponseMessage result = await client.GetAsync(getURL);

                if (result.IsSuccessStatusCode)
                {
                    var deckResponse = result.Content.ReadAsStringAsync().Result;
                    var cards = JArray.Parse(deckResponse);
                    
                    foreach (var card in cards)
                    {
                        switch ((CardType)Enum.Parse(typeof(CardType), card.ToObject<JObject>().GetValue("type").ToString()))
                        {
                            case CardType.Senator:
                                deck.Add(card.ToObject<Senator>());
                                break;
                            case CardType.Statesman:
                                deck.Add(card.ToObject<Statesman>());
                                break;
                            case CardType.Concession:
                                deck.Add(card.ToObject<Concession>());
                                break;
                            case CardType.Intrigue:
                                deck.Add(card.ToObject<Intrigue>());
                                break;
                            case CardType.War:
                                deck.Add(card.ToObject<War>());
                                break;
                            case CardType.Leader:
                                deck.Add(card.ToObject<Leader>());
                                break;
                        }
                    }
                }

                return deck;
            }
        }

        public async Task<List<Card>> GetDeckForGame(long gameId)
        {
            var deck = new List<Card>();
            var getURL = "api/Games/" + gameId.ToString() + "/Cards";

            using (var client = HttpClientHelper.GetHttpClient())
            {
                HttpResponseMessage result = await client.GetAsync(getURL);

                if (result.IsSuccessStatusCode)
                {
                    var deckResponse = result.Content.ReadAsStringAsync().Result;
                    var cards = JArray.Parse(deckResponse);

                    foreach (var card in cards)
                    {
                        switch ((CardType)Enum.Parse(typeof(CardType), card.ToObject<JObject>().GetValue("type").ToString()))
                        {
                            case CardType.Senator:
                                deck.Add(card.ToObject<Senator>());
                                break;
                            case CardType.Statesman:
                                deck.Add(card.ToObject<Statesman>());
                                break;
                            case CardType.Concession:
                                deck.Add(card.ToObject<Concession>());
                                break;
                            case CardType.Intrigue:
                                deck.Add(card.ToObject<Intrigue>());
                                break;
                            case CardType.War:
                                deck.Add(card.ToObject<War>());
                                break;
                            case CardType.Leader:
                                deck.Add(card.ToObject<Leader>());
                                break;
                        }
                    }
                }

                return deck;
            }
        }

        public async Task<List<Card>> CreateDeck(long gameId, List<Card> cards)
        {
            var deck = new List<Card>();
            var postURL = "api/Games/" + gameId + "/Cards";

            using (var client = HttpClientHelper.GetHttpClient())
            {
                HttpResponseMessage result = await client.PostAsJsonAsync(postURL, cards);

                if (result.IsSuccessStatusCode)
                {
                    var deckResponse = result.Content.ReadAsStringAsync().Result;
                    deck = JsonConvert.DeserializeObject<List<Card>>(deckResponse);
                }

                return deck;
            }
        }

        public async Task<Card> UpdateCard(long gameId, Card card)
        {
            var postURL = "api/Games/" + gameId + "/Cards/" + card.CardNo;

            using (var client = HttpClientHelper.GetHttpClient())
            {
                HttpResponseMessage result = await client.PutAsJsonAsync(postURL, card);

                if (result.IsSuccessStatusCode)
                {
                    var response = result.Content.ReadAsStringAsync().Result;
                    card = JsonConvert.DeserializeObject<Card>(response);
                }

                return card;
            }
        }
    }
}