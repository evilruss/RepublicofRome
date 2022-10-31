using RoRService.Models.DataModels;
using RoRService.Repositories.Contracts;
using RoRService.Repositories.Helpers;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace RoRService.WebAPIControllers
{
    public class RepublicsController : ApiController
    {
        private IRepublicsRepository republicsRepo;

        public RepublicsController(IRepublicsRepository republicsRepo)
        {
            this.republicsRepo = republicsRepo;
        }

        // api/Games/{gameId}/Republics
        // Get the Republic for a given game.
        [HttpGet]
        public IHttpActionResult Get(long gameId)
        {
            try
            {
                var factions = republicsRepo.GetRepublic(gameId);

                return Ok(factions);
            }
            catch (Exception e)
            {

                Console.WriteLine(e.ToString());
                return InternalServerError();
            }
        }

        // api/Games/{gameId}/Republics
        // Create the Republic for a given game.
        [HttpPost]
        public IHttpActionResult Post(long gameId, Republic republic)
        {
            try
            {
                if (gameId == 0 || republic == null)
                {
                    return BadRequest();
                }

                var result = republicsRepo.CreateRepublic(gameId, republic);

                if (result.Status == RepositoryActionStatus.Created)
                {
                    return CreatedAtRoute("GamesRoutes",
                                          new
                                          {
                                              gameId = gameId,
                                              controller = "Republics"
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

        // api/Games/{gameId}/Republics
        // Update the Republic for a given game.
        [HttpPut]
        public IHttpActionResult Put(long gameId, Republic republic)
        {
            try
            {
                if (gameId == 0 || republic == null)
                {
                    return BadRequest();
                }

                var result = republicsRepo.UpdateRepublic(gameId, republic);

                if (result.Status == RepositoryActionStatus.Updated)
                {
                    return CreatedAtRoute("GamesRoutes",
                                          new
                                          {
                                              gameId = gameId,
                                              controller = "Republics"
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
