using Newtonsoft.Json.Linq;
using RoRService.Models.DataModels.Cards;
using RoRService.Models.DataModels.Enums;
using RoRService.Repositories.Contracts;
using RoRService.Repositories.Helpers;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace RoRService.WebAPIControllers
{
    public class CardsController : ApiController
    {
        private ICardsRepository cardsRepo;

        public CardsController(ICardsRepository cardsRepo)
        {
            this.cardsRepo = cardsRepo;
        }

        [HttpGet]
        public IHttpActionResult Get()
        {
            try
            {
                var cardList = cardsRepo.GetCards();

                if (cardList == null || cardList.Count == 0)
                {
                    return NotFound();

                }
                else
                {
                    return Ok(cardList);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return InternalServerError();
            }
        }

        [HttpGet]
        public IHttpActionResult Get(Era id)
        {
            try
            {
                var cardList = cardsRepo.GetCardsByEra(id);

                return Ok(cardList);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return InternalServerError();
            }
        }

        [HttpGet]
        public IHttpActionResult Get(long gameId)
        {
            try
            {
                var cardList = cardsRepo.GetAllCardsForGame(gameId);

                return Ok(cardList);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return InternalServerError();
            }
        }

        [HttpPost]
        public IHttpActionResult Post(long gameId, List<CardVariables> cardVariableList)
        {
            try
            {
                if (cardVariableList == null || gameId == 0)
                {
                    return BadRequest();
                }

                var result = cardsRepo.CreateCardVariables(cardVariableList, gameId);

                if (result.Status == RepositoryActionStatus.Created)
                {
                    return CreatedAtRoute("GamesRoutes",
                                          new
                                          {
                                              gameId = gameId,
                                              controller = "Cards"
                                          },
                                          result.Entity);
                }

                return BadRequest();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return InternalServerError();
            }
        }

        [HttpPut]
        public IHttpActionResult Put(long gameId, int id, JObject jCard)
        {
            try
            {
                if (gameId == 0 || id == 0 || jCard == null)
                {
                    return BadRequest();
                }

                var cardVariables = new CardVariables()
                {
                    CardNo = id,
                    GameId = gameId,
                    Influence = (Int32)jCard.GetValue("Influence"),
                    Popularity = (Int32)jCard.GetValue("Popularity"),
                    Knights = (Int32)jCard.GetValue("Knights"),
                    PriorConsul = (bool)jCard.GetValue("PriorConsul"),
                    Treasury = (Int32)jCard.GetValue("Treasury")
                };

                var result = cardsRepo.UpdateCardVariables(cardVariables);

                if (result.Status == RepositoryActionStatus.Updated)
                {
                    return CreatedAtRoute("GamesRoutes",
                                          new
                                          {
                                              gameId = gameId,
                                              id = id,
                                              controller = "Cards"
                                          },
                                          result.Entity);
                }

                return BadRequest();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return InternalServerError();
            }
        }
    }
}