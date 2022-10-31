using RoRService.Models.DataModels;
using RoRService.Models.DataModels.Cards;
using RoRService.Models.DataModels.Enums;
using RoRService.Resources;
using RoRService.Resources.Contracts;
using System;
using System.Threading.Tasks;

namespace RoRService.Engines
{
    public class GameOperations
    {
        protected ICardsResource cardsResource;
        protected IFactionsResource factionsResource;
        protected IGamesResource gamesResource;
        protected IOfficesResource officesResource;
        protected IPlayersResource playersResource;
        protected IRepublicsResource republicsResource;
        protected ILegionsResource legionsResource;

        protected Random rnd;

        public const int LegionFleetTotal = 24;

        public GameOperations()
        {
            cardsResource = new CardsResource();
            factionsResource = new FactionsResource();
            gamesResource = new GamesResource();
            officesResource = new OfficesResource();
            playersResource = new PlayersResource();
            republicsResource = new RepublicsResource();
            legionsResource = new LegionsResource();
            rnd = new Random();
        }

        public GameOperations(ICardsResource cardsResource, 
                              IFactionsResource factionsResource, 
                              IGamesResource gamesResource, 
                              IPlayersResource playersResource, 
                              IOfficesResource officesResource, 
                              IRepublicsResource republicsResource,
                              ILegionsResource legionsResource)
        {
            this.cardsResource = cardsResource;
            this.factionsResource = factionsResource;
            this.gamesResource = gamesResource;
            this.officesResource = officesResource;
            this.playersResource = playersResource;
            this.republicsResource = republicsResource;
            this.legionsResource = legionsResource;
            rnd = new Random();
        }

        public async Task AppointOfficer(long gameId, OfficeTitle officeTitle, Senator senator)
        {
            Office office = await officesResource.GetOffice(officeTitle);
            office.CardNo = senator.CardNo;
            await officesResource.UpdateOffice(gameId, office);

            senator.Influence += office.Influence;
            await cardsResource.UpdateCard(gameId, senator);
        }

        public async Task UpdateRepublic(long gameId, Republic republic)
        {
            await republicsResource.UpdateRepublic(gameId, republic);
        }

    }
}