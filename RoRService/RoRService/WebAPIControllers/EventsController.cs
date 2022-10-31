using RoRService.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace RoRService.WebAPIControllers
{
    public class EventsController : ApiController
    {
        private IEventsRepository eventsRepo;

        public EventsController(IEventsRepository eventsRepo)
        {
            this.eventsRepo = eventsRepo;
        }

        [Route("api/Events/{eventId}/Level/{eventLevel}")]
        [HttpGet]
        public IHttpActionResult Get(int eventId, int eventLevel)
        {
            try
            {
                var newEvent = eventsRepo.GetEvent(eventId, eventLevel);

                if (newEvent == null)
                {
                    return NotFound();
                }
                else
                {
                    return Ok(newEvent);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return InternalServerError();
            }
        }

        [Route("api/Games/{gameId}/Events")]
        [HttpGet]
        public IHttpActionResult Get(int gameId)
        {
            try
            {
                var eventsList = eventsRepo.GetEventsForGame(gameId);

                if (eventsList == null || eventsList.Count == 0)
                {
                    return NotFound();
                }
                else
                {
                    return Ok(eventsList);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return InternalServerError();
            }
        }
    }
}
