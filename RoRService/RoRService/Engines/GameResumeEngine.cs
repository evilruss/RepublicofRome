using AutoMapper;
using RoRService.Models.DataModels;
using RoRService.Models.DataModels.Cards;
using RoRService.Models.DataModels.Enums;
using RoRService.Models.ViewModels;
using RoRService.Models.ViewModels.CardsViewModels;
using RoRService.Models.ViewModels.FactionsViewModels;
using RoRService.Models.ViewModels.GameBoardViewModels;
using RoRService.Models.ViewModels.RepublicsViewModels;
using RoRService.Resources;
using RoRService.Resources.Contracts;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RoRService.Engines
{
    public class GameResumeEngine
    {
        ICardsResource cardsResource;
        IEventsResource eventsResource;
        IFactionsResource factionsResource;
        IGamesResource gamesResource;
        IPlayersResource playersResource;
        IRepublicsResource republicsResource;

        public GameResumeEngine()
        {
            cardsResource = new CardsResource();
            eventsResource = new EventsResource();
            factionsResource = new FactionsResource();
            gamesResource = new GamesResource();
            playersResource = new PlayersResource();
            republicsResource = new RepublicsResource();
        }

        public GameResumeEngine(ICardsResource cardsResource,
                                IEventsResource eventsResource,
                                IFactionsResource factionsResource, 
                                IGamesResource gamesResource, 
                                IPlayersResource playersResource,
                                IRepublicsResource republicsResource)
        {
            this.cardsResource = cardsResource;
            this.eventsResource = eventsResource;
            this.factionsResource = factionsResource;
            this.gamesResource = gamesResource;
            this.playersResource = playersResource;
            this.republicsResource = republicsResource;
        }

        public async Task<GameBoardVM> ResumeGame(long gameId)
        {
            var gameBoardVM = new GameBoardVM();
            var game = await gamesResource.GetGameDetails(gameId);
            var factionList = await factionsResource.GetFactions(gameId);
            var republic = await republicsResource.GetRepublic(gameId);
            republic.Deck = await cardsResource.GetDeckForGame(game.Id);
            republic.Events = await eventsResource.GetEventsForGame(game.Id);

            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<Card, CardVM>();
                cfg.CreateMap<Concession, ConcessionVM>();
                cfg.CreateMap<Event, EventVM>();
                cfg.CreateMap<Faction, FactionVM>();
                cfg.CreateMap<Intrigue, IntrigueVM>();
                cfg.CreateMap<Office, OfficeVM>();
                cfg.CreateMap<Republic, RepublicVM>();
                cfg.CreateMap<Senator, SenatorVM>();
                cfg.CreateMap<Statesman, StatesmanVM>();
                cfg.CreateMap<War, WarVM>();
            });
            var iMapper = config.CreateMapper();

            gameBoardVM.FactionList = ConvertFactionListToVM(factionList, iMapper);
            gameBoardVM.Republic = iMapper.Map<Republic, RepublicVM>(republic);
            gameBoardVM.Republic.Deck = ConvertCardListToVM(republic.Deck, iMapper); 

            return gameBoardVM;
        }

        public List<FactionVM> ConvertFactionListToVM (List<Faction> factionList, IMapper iMapper)
        {
            var factionListVM = new List<FactionVM>();

            foreach (var faction in factionList)
            {
                var factionVM = iMapper.Map<Faction, FactionVM>(faction);
                factionVM.Senators = ConvertCardListToVM(faction.Senators, iMapper);
                factionVM.Hand = ConvertCardListToVM(faction.Hand, iMapper);
                factionListVM.Add(factionVM);
            }

            return factionListVM;
        }

        public List<CardVM> ConvertCardListToVM(List<Card> cardList, IMapper iMapper)
        {
            var cardListVM = new List<CardVM>();

            foreach (var card in cardList)
            {
                switch (card.Type)
                {
                    case CardType.Senator:
                        var senatorVM = iMapper.Map<Senator, SenatorVM>((Senator)card);
                        cardListVM.Add(senatorVM);
                        break;
                    case CardType.Statesman:
                        var statesmanVM = iMapper.Map<Statesman, StatesmanVM>((Statesman)card);
                        cardListVM.Add(statesmanVM);
                        break;
                    case CardType.Concession:
                        var concessionVM = iMapper.Map<Concession, ConcessionVM>((Concession)card);
                        cardListVM.Add(concessionVM);
                        break;
                    case CardType.Intrigue:
                        var intrigueVM = iMapper.Map<Intrigue, IntrigueVM>((Intrigue)card);
                        cardListVM.Add(intrigueVM);
                        break;
                    case CardType.War:
                        var warVM = iMapper.Map<War, WarVM>((War)card);
                        cardListVM.Add(warVM);
                        break;
                    //case CardType.Leader:
                    //    var leaderVM = iMapper.Map<Leader, LeaderVM>((Leader)card);
                    //    cardListVM.Add(leaderVM);
                    //    break;
                }
            }

             return cardListVM;
        }
    }
}