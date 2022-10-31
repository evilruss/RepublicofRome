using RoRService.Engines.Contracts;
using RoRService.Models.DataModels;
using RoRService.Models.DataModels.Cards;
using RoRService.Models.DataModels.Enums;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace RoRService.Engines
{
    public class GameStartUpEngine : GameOperations, IGameStartUpEngine
    {
        public async Task SetUpNewGame(long gameId)
        {
            var game = await gamesResource.GetGameDetails(gameId);
            var deck = await cardsResource.GetNewDeck(game.Era);
            var factionList = BuildFactions(game, deck);
            var factions = await factionsResource.CreateFactions(gameId, factionList);
            await CreateGameDeck(game, deck);
            await CreateOffices(gameId);
            await CreateRepublic(game);
            await AppointTemporaryRomeConsul(gameId, deck);
            await SetGameStatusToPlaying(game);
        }

        private List<Faction> BuildFactions(Game game, List<Card> cardlist)
        {
            var factionList = new List<Faction>();
            var factionNo = 0;
            var senatorsPerFaction = 0;
            var intriguesPerFaction = 3;

            switch (game.Era)
            {
                case Era.Early:
                    senatorsPerFaction = 3;
                    break;
                case Era.Middle:
                    senatorsPerFaction = 4;
                    break;
                case Era.Late:
                    senatorsPerFaction = 5;
                    break;
            }

            foreach (Player player in game.PlayerList)
            {
                factionNo++;

                var faction = new Faction()
                {
                    No = factionNo,
                    GameId = game.Id,
                    PlayerId = player.Id,
                    Treasury = 0,
                    Senators = AllocateCards(cardlist, new CardType[] { CardType.Senator }, senatorsPerFaction, CardStatus.InPlay),
                    Hand = AllocateCards(cardlist, new CardType[] { CardType.Statesman, CardType.Concession, CardType.Intrigue }, intriguesPerFaction, CardStatus.Hand)

                };

                faction.Leader = faction.Senators.Cast<Senator>().OrderBy(s => s.FamilyNo).First().FamilyNo;
                factionList.Add(faction);
            }

            return factionList;
        }

        private List<Card> AllocateCards(List<Card> deck, CardType[] cardTypes, int noOfCards, CardStatus cardStatus)
        {
            var cardList = new List<Card>();
            var cards = deck.Where(s => (cardTypes.Contains(s.Type)) && (s.Status == CardStatus.Deck)).ToList();

            for (int i = 0; i < noOfCards; i++)
            {
                int cardPointer = rnd.Next(0, cards.Count());

                var card = deck.Where(s => s.CardNo == cards[cardPointer].CardNo).First();
                card.Status = cardStatus;
                cardList.Add(card);
                cards.Remove(card);
            }

            return cardList;
        }

        private async Task CreateGameDeck(Game game, List<Card> deck)
        {
            switch (game.Era)
            {
                case Era.Early:
                    // First Punic War (card 118) set to Inactive in the Early Republic game.
                    deck.Where(c => c.CardNo == 118).First().Status = CardStatus.Inactive;
                    break;
                case Era.Middle:
                    break;
                case Era.Late:
                    var middleLaws = deck.Where(c => (c.Era == Era.Middle) && (c.Type == CardType.Law)).ToList();
                    break;
            }

            await cardsResource.CreateDeck(game.Id, deck);
        }

        private async Task CreateOffices(long gameId)
        {
            await officesResource.CreateOffices(gameId);
        }

        private async Task CreateRepublic(Game game)
        {
            var republic = new Republic();
            republic.StateTreasury = 100;
            republic.UnrestLevel = 0;
            republic.Fleets = 0;

            await republicsResource.CreateRepublic(game.Id, republic);

            switch (game.Era)
            {
                case Era.Early:
                    republic.Legions = InitialiseStartingLegions(4);
                    republic.Laws = string.Empty;
                    break;
                case Era.Middle:
                    republic.Legions = InitialiseStartingLegions(4);
                    republic.Laws = string.Empty;
                    break;
                case Era.Late:
                    republic.Legions = InitialiseStartingLegions(8);
                    republic.Laws = "TO DO: Middle Republic Laws played - change me!";
                    break;
            }

            await legionsResource.CreateLegions(game.Id, republic.Legions);


        }

        private async Task AppointTemporaryRomeConsul(long gameId, List<Card> deck)
        {
            List<Senator> senatorDeck = deck.Where(c => c.Type == CardType.Senator && c.Status == CardStatus.InPlay).Cast<Senator>().ToList();
            var romeConsul = senatorDeck.OrderBy(c => c.FamilyNo).First();
            await AppointOfficer(gameId, OfficeTitle.RomeConsul, romeConsul);
        }

        private async Task SetGameStatusToPlaying(Game game)
        {
            game.Status = GameStatus.Playing;
            await gamesResource.UpdateGame(game);
        }

        // Unlike fleets, Legions have two possible statuses when active, Recruited (normal) and Veteran.
        // In addition, Veteran Legions can be owned by a Senator.
        // As a result, it is necessary to keep track of their status throughout the game.
        // This method creates all possible Legions available in the game, defaulting them to Disbanded status
        // except for the number of basic legions Rome starts with as Recruited at the start of the game,
        // which is passed in as a variable.
        private List<Legion> InitialiseStartingLegions(int noOfRecruitedLegions)
        {
            List<Legion> legionList = new List<Legion>();

            for (int i = 1; i <= LegionFleetTotal;  i++)
            {
                var legion = new Legion();
                if (i <= noOfRecruitedLegions)
                {
                    legion.Status = LegionStatus.Recruited;
                }
                else
                {
                    legion.Status = LegionStatus.Disbanded;
                }
            }

            return legionList;
        }
    }
}
