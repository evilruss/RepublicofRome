using RoRService.Models.DataModels;
using RoRService.Repositories.Contracts;
using RoRService.Repositories.Helpers;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace RoRService.WebAPIControllers
{
    public class LegionsController : ApiController
    {
        private ILegionsRepository legionsRepo;

        public LegionsController(ILegionsRepository legionsRepo)
        {
            this.legionsRepo = legionsRepo;
        }

        // api/Games/{gameId}/Republics/{Id}/Legions
        // Get the Legions for a given game.
        [HttpGet]
        public IHttpActionResult Get(long gameId)
        {
            try
            {
                var factions = legionsRepo.GetLegions(gameId);

                return Ok(factions);
            }
            catch (Exception e)
            {

                Console.WriteLine(e.ToString());
                return InternalServerError();
            }
        }

        // api/Games/{gameId}/Legions
        // Creates the Legions for a given game.
        [HttpPost]
        public IHttpActionResult Post(long gameId, [FromBody]List<Legion> newLegionList)
        {
            try
            {
                if (newLegionList == null)
                {
                    return BadRequest();
                }

                var result = legionsRepo.CreateLegions(newLegionList);

                if (result.Status == RepositoryActionStatus.Created)
                {
                    return CreatedAtRoute("GamesRoutes",
                                          new
                                          {
                                              controller = "Legions",
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
