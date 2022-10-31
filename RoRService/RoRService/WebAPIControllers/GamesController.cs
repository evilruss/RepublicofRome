using RoRService.Models.DataModels;
using RoRService.Repositories.Contracts;
using System.Web.Http;
using System;
using System.Linq;
using RoRService.Repositories.Helpers;
using Newtonsoft.Json.Linq;

namespace RoRService.WebAPIControllers
{
    public class GamesController : ApiController
    {
        private IGamesRepository gamesRepo;
        private IPlayersRepository playersRepo;

        public GamesController(IGamesRepository gameRepo, IPlayersRepository playerRepo)
        {
            this.gamesRepo = gameRepo;
            this.playersRepo = playerRepo;
        }

        public IHttpActionResult Get()
        {
            try
            {
                var gameList = gamesRepo.GetGames();

                if (gameList == null || gameList.Count() == 0)
                {
                    return NotFound();
                }
                else
                {
                    return Ok(gameList);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return InternalServerError();
            }
        }

        public IHttpActionResult Get(long id)
        {
            try
            {
                var game = gamesRepo.GetGame(id);

                if (game == null)
                {
                    return NotFound();
                }
                else
                {

                    game.PlayerList = playersRepo.GetPlayersByGameId(id).ToList();

                    return Ok(game);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return InternalServerError();
            }
        }

        [HttpPost]
        public IHttpActionResult Post(Game newGame)
        {
            try
            {
                if (newGame == null || newGame.PlayerList == null || newGame.PlayerList.Count == 0)
                {
                    return BadRequest();
                }

                var gameResult = gamesRepo.CreateGame(newGame);

                if (gameResult.Status == RepositoryActionStatus.Created)
                {
                    newGame.PlayerList[0].GameId = gameResult.Entity.Id;

                    var playerResult = playersRepo.CreatePlayer(newGame.PlayerList[0]);

                    if (playerResult.Status == RepositoryActionStatus.Created)
                    {
                        var result = CreatedAtRoute("DefaultApi",
                                              new
                                              {
                                                  controller = "Games",
                                                  id = gameResult.Entity.Id
                                              },
                                              gameResult.Entity);

                        return result;

                    }

                    return BadRequest();
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
        public IHttpActionResult Put([FromBody]Game newGame)
        {
            try
            {
                if (newGame == null)
                {
                    return BadRequest();
                }

                var gameResult = gamesRepo.UpdateGame(newGame);

                if (gameResult.Status == RepositoryActionStatus.Created)
                {
                    return CreatedAtRoute("DefaultApi",
                                            new
                                            {
                                                controller = "Games",
                                                id = gameResult.Entity.Id
                                            },
                                            gameResult.Entity);
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
