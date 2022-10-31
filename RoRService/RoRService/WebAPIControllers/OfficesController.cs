using RoRService.Models.DataModels;
using RoRService.Repositories.Contracts;
using RoRService.Repositories.Helpers;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace RoRService.WebAPIControllers
{
    public class OfficesController : ApiController
    {
        private IOfficesRepository officesRepo;

        public OfficesController(IOfficesRepository officesRcepo)
        {
            this.officesRepo = officesRcepo;
        }

        public IHttpActionResult Get()
        {
            try
            {
                var officeList = officesRepo.GetOffices();

                return Ok(officeList);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return InternalServerError();
            }
        }

        public IHttpActionResult Get(int id)
        {
            try
            {
                var office = officesRepo.GetOffice(id);

                if (office == null)
                {
                    return NotFound();
                }
                else
                {
                    return Ok(office);
                }
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
                var officeList = officesRepo.GetOfficesForGame(gameId);

                return Ok(officeList);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return InternalServerError();
            }
        }

        [HttpGet]
        public IHttpActionResult Get(long gameId, int id)
        {
            try
            {
                var office = officesRepo.GetOfficeForGame(gameId, id);

                return Ok(office);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return InternalServerError();
            }
        }

        [HttpPost]
        public IHttpActionResult Post(long gameId, [FromBody]Office office)
        {
            try
            {
                if (gameId == 0)
                {
                    return BadRequest();
                }

                var result = officesRepo.CreateOffice(office);

                if (result.Status != RepositoryActionStatus.Created)
                {
                    return BadRequest();
                }

                return CreatedAtRoute("GamesRoutes",
                                      new
                                      {
                                          gameId = gameId,
                                          id = result.Entity.OfficeId,
                                          controller = "Offices"
                                      },
                                      result.Entity);

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return InternalServerError();
            }
        }

        [HttpPut]
        public IHttpActionResult Put(long gameId, int id, [FromBody]Office office)
        {
            try
            {
                if (office == null)
                {
                    return BadRequest();
                }

                var result = officesRepo.UpdateOffice(gameId, id, office);

                if (result.Status == RepositoryActionStatus.Updated)
                {
                    return Ok(office);
                }
                else if (result.Status == RepositoryActionStatus.NotFound)
                {
                    return NotFound();
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
