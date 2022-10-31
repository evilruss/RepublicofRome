using RoRService.Models.DataModels;
using RoRService.Repositories;
using RoRService.Repositories.Helpers;
using RoRService.Repositories.Contracts;
using System;
using System.Web.Http;

namespace RoRService.WebAPIControllers
{
    public class PlayersController : ApiController
    {
        private IPlayersRepository playerRepo;

        public PlayersController(IPlayersRepository playerRepo)
        {
            this.playerRepo = playerRepo;
        }

        [HttpGet]
        [Route("api/Games/{gameId}/Players")]
        public IHttpActionResult Get(long gameId)
        {
            try
            {
                var player = playerRepo.GetPlayersByGameId(gameId);

                if (player == null)
                {
                    return NotFound();
                }
                else
                {
                    return Ok(player);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return InternalServerError();
            }
        }

        public IHttpActionResult Get(long gameId, long id)
        {
            try
            {
                var player = playerRepo.GetPlayer(id);

                if (player == null)
                {
                    return NotFound();
                }
                else
                {
                    return Ok(player);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return InternalServerError();
            }
        }

        [HttpPost]
        [Route("api/Games/{gameId}/Players")]
        public IHttpActionResult Post(long gameId, [FromBody]Player newPlayer)
        {
            try
            {
                if (newPlayer == null)
                {
                    return BadRequest();
                }

                newPlayer.GameId = gameId;
                var playerResult = playerRepo.CreatePlayer(newPlayer);

                if (playerResult.Status == RepositoryActionStatus.Created)
                {
                    string routeURL = Request.RequestUri + "/" + playerResult.Entity.Id.ToString();
                    var createdPlayer = playerResult.Entity;
                    var returnValue = CreatedAtRoute(
                        routeName: "GamesRoutes",
                        routeValues: new
                        {
                            gameId = gameId,
                            id = createdPlayer.Id,
                            controller = "Players"
                        },
                        content: createdPlayer);

                    return returnValue;
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
