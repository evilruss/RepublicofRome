using RoRService.Models.DataModels;
using RoRService.Repositories;
using RoRService.Repositories.Contracts;
using RoRService.Repositories.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace RoRService.WebAPIControllers
{
    public class FactionsController : ApiController
    {
        private IFactionsRepository factionRepo;

        public FactionsController(IFactionsRepository factionRepo)
        {
            this.factionRepo = factionRepo;
        }

        // api/Games/{gameId}/Factions
        // Gets a list of all Factions for a given game.
        [HttpGet]
        public IHttpActionResult Get(long gameId)
        {
            try
            {
                var factions = factionRepo.GetFactions(gameId);

                return Ok(factions);
            }
            catch (Exception e)
            {

                Console.WriteLine(e.ToString());
                return InternalServerError();
            }
        }

        // api/Games/{gameId}/Factions/{id}
        // Gets details of one faction for a given game.
        [HttpGet]
        public IHttpActionResult Get(long gameId, int id)
        {
            try
            {
                var faction = factionRepo.GetFaction(gameId, id);

                if (faction == null)
                {
                    return NotFound();
                }
                else
                {
                    return Ok(faction);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return InternalServerError();
            }
        }

        // api/Games/{gameId}/Factions
        // Creates the Factions for a given game.
        [HttpPost]
        public IHttpActionResult Post(long gameId, [FromBody]List<Faction> newFactionsList)
        {
            try
            {
                if (newFactionsList == null)
                {
                    return BadRequest();
                }

                var result = factionRepo.CreateFactions(newFactionsList);

                if (result.Status == RepositoryActionStatus.Created)
                {
                    return CreatedAtRoute("GamesRoutes",
                                          new
                                          {
                                              controller = "Factions",
                                              gameId = gameId,
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
