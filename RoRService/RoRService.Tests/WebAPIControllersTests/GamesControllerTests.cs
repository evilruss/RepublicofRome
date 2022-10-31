using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using RoRService.Models.DataModels;
using RoRService.Repositories.Contracts;
using RoRService.WebAPIControllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Results;
using System.Net.Http;
using RoRService.Repositories.Helpers;

namespace RoRService.Tests.WebAPIControllersTests
{
    [TestClass]
    public class GamesControllerTests
    {
        Mock<IGamesRepository> mockGameRepository;
        Mock<IPlayersRepository> mockPlayerRepository;
        GamesController controller;
        Game testGame;
        List<Game> testGameList;
        Int64 gameId;

        [TestInitialize]
        public void TestSetup()
        {
            gameId = 10;

            testGame = new Game
            {
                Id = gameId,
                Name = "My Game",
                PlayerList = new List<Player>
                {
                    new Player
                    {
                        Name = "Player 1",
                        FactionName = "Faction 1"
                    }
                }
            };

            testGameList = new List<Game>()
                {
                    testGame,
                    testGame,
                    testGame
                };

            mockGameRepository = new Mock<IGamesRepository>();
            mockPlayerRepository = new Mock<IPlayersRepository>();

            controller = new GamesController(mockGameRepository.Object, mockPlayerRepository.Object);
            controller.Request = new HttpRequestMessage();
            controller.Configuration = new HttpConfiguration();

        }

        [TestMethod]
        public void GamesGetReturnsOk()
        {
            mockGameRepository.Setup(x => x.GetGames())
                .Returns(testGameList);

            IHttpActionResult actionResult = controller.Get();
            var contentResult = actionResult as OkNegotiatedContentResult<IEnumerable<Game>>;

            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(contentResult.Content);
            Assert.AreEqual(3, contentResult.Content.ToList().Count);
        }


        [TestMethod]
        public void GamesGetWithIdReturnsOk()
        {
            mockGameRepository.Setup(x => x.GetGame(gameId))
                .Returns(testGame);

            IHttpActionResult actionResult = controller.Get(gameId);
            var contentResult = actionResult as OkNegotiatedContentResult<Game>;

            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(contentResult.Content);
            Assert.AreEqual(10, contentResult.Content.Id);
        }

        [TestMethod]
        public void GamesGetWithIdReturnsNotFound()
        {
            mockGameRepository.Setup(x => x.GetGame(gameId))
                .Returns((Game)null);

            IHttpActionResult actionResult = controller.Get(gameId);

            Assert.IsInstanceOfType(actionResult, typeof(NotFoundResult));
        }

        [TestMethod]
        public void GamesPostReturnsCreatedAtRoute()
        {
            mockGameRepository.Setup(x => x.CreateGame(testGame))
                .Returns(new RepositoryActionResult<Game>(testGame, RepositoryActionStatus.Created));
            mockPlayerRepository.Setup(x => x.CreatePlayer(testGame.PlayerList[0]))
                .Returns(new RepositoryActionResult<Player>(testGame.PlayerList[0], RepositoryActionStatus.Created));
            controller.Request.Method = HttpMethod.Post;

            IHttpActionResult actionResult = controller.Post(testGame);
            var createdResult = actionResult as CreatedAtRouteNegotiatedContentResult<Game>;

            Assert.IsNotNull(createdResult);
            Assert.AreEqual("DefaultApi", createdResult.RouteName);
            Assert.AreEqual(gameId, createdResult.RouteValues["id"]);
        }

        [TestMethod]
        public void GamesPostReturnsBadRequestForNullPost()
        {
            controller.Request.Method = HttpMethod.Post;

            IHttpActionResult actionResult = controller.Post(null);

            Assert.IsInstanceOfType(actionResult, typeof(BadRequestResult));
        }

        [TestMethod]
        public void GamesPostReturnsBadRequestForGameRepositoryError()
        {
            mockGameRepository.Setup(x => x.CreateGame(testGame))
                .Returns(new RepositoryActionResult<Game>(testGame, RepositoryActionStatus.NothingModified));
            controller.Request.Method = HttpMethod.Post;

            IHttpActionResult actionResult = controller.Post(testGame);

            Assert.IsInstanceOfType(actionResult, typeof(BadRequestResult));
        }

        [TestMethod]
        public void GamesPostReturnsBadRequestForPlayerRepositoryError()
        {
            mockGameRepository.Setup(x => x.CreateGame(testGame))
                .Returns(new RepositoryActionResult<Game>(testGame, RepositoryActionStatus.Created));
            mockPlayerRepository.Setup(x => x.CreatePlayer(testGame.PlayerList[0]))
                .Returns(new RepositoryActionResult<Player>(testGame.PlayerList[0], RepositoryActionStatus.NothingModified));

            controller.Request.Method = HttpMethod.Post;

            IHttpActionResult actionResult = controller.Post(testGame);

            Assert.IsInstanceOfType(actionResult, typeof(BadRequestResult));
        }

    }
}