using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using RoRService.Models.DataModels;
using RoRService.Repositories.Contracts;
using RoRService.Repositories.Helpers;
using RoRService.WebAPIControllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Results;

namespace RoRService.Tests.WebAPIControllersTests
{
    [TestClass]
    public class PlayersControllerTests
    {
        Mock<IPlayersRepository> mockPlayerRepository;
        PlayersController controller;
        Player testPlayer;
        List<Player> testPlayerList;
        Int64 gameId;
        Int64 playerId;

        [TestInitialize]
        public void TestSetup()
        {
            gameId = 11;
            playerId = 10;

            testPlayer = new Player
            {
                Id = playerId,
                Name = "Player Name",
                FactionName = "My Faction"
            };

            testPlayerList = new List<Player>()
                {
                    testPlayer,
                    testPlayer,
                    testPlayer
                };

            mockPlayerRepository = new Mock<IPlayersRepository>();

            controller = new PlayersController(mockPlayerRepository.Object);
            controller.Request = new HttpRequestMessage();
            controller.Configuration = new HttpConfiguration();

        }

        //No test for basic Get as no game reason to ever call it.

        [TestMethod]
        public void PlayersGetByGameIdReturnsOk()
        {
            mockPlayerRepository.Setup(x => x.GetPlayersByGameId(playerId))
                .Returns(testPlayerList);

            IHttpActionResult actionResult = controller.Get(playerId);
            var contentResult = actionResult as OkNegotiatedContentResult<IEnumerable<Player>>;

            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(contentResult.Content);
            Assert.AreEqual(3, contentResult.Content.ToList().Count);
        }

        [TestMethod]
        public void PlayersGetByGameIdReturnsNotFound()
        {
            mockPlayerRepository.Setup(x => x.GetPlayersByGameId(playerId))
                .Returns((List<Player>)null);

            IHttpActionResult actionResult = controller.Get(playerId);

            Assert.IsInstanceOfType(actionResult, typeof(NotFoundResult));
        }

        [TestMethod]
        public void PlayersGetReturnsOk()
        {
            mockPlayerRepository.Setup(x => x.GetPlayer(playerId))
                .Returns(testPlayer);

            IHttpActionResult actionResult = controller.Get(gameId, playerId);
            var contentResult = actionResult as OkNegotiatedContentResult<Player>;

            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(contentResult.Content);
            Assert.AreEqual(playerId, contentResult.Content.Id);
        }

        [TestMethod]
        public void PlayersGetPlayerReturnsNotFound()
        {
            mockPlayerRepository.Setup(x => x.GetPlayer(gameId))
                .Returns((Player)null);

            IHttpActionResult actionResult = controller.Get(gameId, playerId);

            Assert.IsInstanceOfType(actionResult, typeof(NotFoundResult));
        }

        [TestMethod]
        public void PlayersPostReturnsCreatedAtRoute()
        {
            Int64 gameId = 11;
            mockPlayerRepository.Setup(x => x.CreatePlayer(testPlayer))
                .Returns(new RepositoryActionResult<Player>(testPlayer, RepositoryActionStatus.Created));
            controller.Request.Method = HttpMethod.Post;
            controller.Request.RequestUri = new Uri("http://localhost/api/Games/" + gameId + "/Players");

            IHttpActionResult actionResult = controller.Post(gameId, testPlayer);
            var createdResult = actionResult as CreatedAtRouteNegotiatedContentResult<Player>;

            Assert.IsNotNull(createdResult);
            Assert.AreEqual(gameId, createdResult.Content.GameId);
            Assert.AreEqual("GamesRoutes", createdResult.RouteName);
            Assert.AreEqual(gameId, createdResult.RouteValues["gameId"]);
            Assert.AreEqual(playerId, createdResult.RouteValues["id"]);
        }

        [TestMethod]
        public void PlayersPostReturnsBadRequestForNullPost()
        {
            controller.Request.Method = HttpMethod.Post;

            IHttpActionResult actionResult = controller.Post(0, null);

            Assert.IsInstanceOfType(actionResult, typeof(BadRequestResult));
        }

        [TestMethod]
        public void PlayersPostReturnsBadRequestForPlayerRepositoryError()
        {
            mockPlayerRepository.Setup(x => x.CreatePlayer(testPlayer))
                .Returns(new RepositoryActionResult<Player>(testPlayer, RepositoryActionStatus.NothingModified));

            controller.Request.Method = HttpMethod.Post;

            IHttpActionResult actionResult = controller.Post(11, testPlayer);

            Assert.IsInstanceOfType(actionResult, typeof(BadRequestResult));
        }
    }
}