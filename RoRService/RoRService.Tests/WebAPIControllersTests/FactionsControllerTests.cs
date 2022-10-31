using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using RoRService.Models.DataModels;
using RoRService.Models.DataModels.Cards;
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
    public class FactionsControllerTests
    {
        Mock<IFactionsRepository> mockFactionsRepository;
        FactionsController controller;
        Faction testFaction;
        List<Faction> testFactionList;
        Int64 gameId;
        int factionId;

        [TestInitialize]
        public void TestSetup()
        {
            gameId = 10;
            factionId = 1;

            testFaction = new Faction
            {
                Id = factionId,
                No = 1,
                GameId = gameId,
                PlayerId = 1,
                Name = "TestFaction",
                Treasury = 1,
                Leader = 1,
                Senators = new List<Card>
                {
                    new Senator
                    {
                        CardNo = 1,
                        HasStatesman = true,
                        Oratory = 1,
                        Knights = 0
                    },
                    new Statesman
                    {
                        CardNo = 2,
                        Oratory = 4,
                        Knights = 7
                    }
                },
                Hand = new List<Card>
                {
                    new Concession
                    {
                        CardNo = 50
                    }
                }
            };

            testFactionList = new List<Faction>()
                {
                    testFaction,
                    testFaction,
                    testFaction
                };

            mockFactionsRepository = new Mock<IFactionsRepository>();

            controller = new FactionsController(mockFactionsRepository.Object);
            controller.Request = new HttpRequestMessage();
            controller.Configuration = new HttpConfiguration();

        }

        [TestMethod]
        public void FactionsGetWithGameIdReturnsOk()
        {
            mockFactionsRepository.Setup(x => x.GetFactions(gameId))
                .Returns(testFactionList);

            IHttpActionResult actionResult = controller.Get(gameId);
            var contentResult = actionResult as OkNegotiatedContentResult<IEnumerable<Faction>>;

            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(contentResult.Content);
            Assert.AreEqual(3, contentResult.Content.ToList().Count);
        }


        [TestMethod]
        public void FactionsGetWithGameAndFactionIdReturnsOk()
        {
            mockFactionsRepository.Setup(x => x.GetFaction(gameId, factionId))
                .Returns(testFaction);

            IHttpActionResult actionResult = controller.Get(gameId, factionId);
            var contentResult = actionResult as OkNegotiatedContentResult<Faction>;

            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(contentResult.Content);
            Assert.AreEqual(factionId, contentResult.Content.Id);
        }

        [TestMethod]
        public void FactionsGetWithIdReturnsNotFound()
        {
            mockFactionsRepository.Setup(x => x.GetFaction(gameId, factionId))
                .Returns((Faction)null);

            IHttpActionResult actionResult = controller.Get(gameId, factionId);

            Assert.IsInstanceOfType(actionResult, typeof(NotFoundResult));
        }

        [TestMethod]
        public void FactionsPostReturnsCreatedAtRoute()
        {
            mockFactionsRepository.Setup(x => x.CreateFactions(testFactionList))
                .Returns(new RepositoryActionResult<List<Faction>>(testFactionList, RepositoryActionStatus.Created));
            controller.Request.Method = HttpMethod.Post;

            IHttpActionResult actionResult = controller.Post(gameId, testFactionList);
            var createdResult = actionResult as CreatedAtRouteNegotiatedContentResult<List<Faction>>;

            Assert.IsNotNull(createdResult);
            Assert.AreEqual("GamesRoutes", createdResult.RouteName);
            Assert.AreEqual(3, createdResult.Content.ToList().Count);
        }

        [TestMethod]
        public void FactionsPostReturnsBadRequestForNullPost()
        {
            controller.Request.Method = HttpMethod.Post;

            IHttpActionResult actionResult = controller.Post(gameId, null);

            Assert.IsInstanceOfType(actionResult, typeof(BadRequestResult));
        }
    }
}