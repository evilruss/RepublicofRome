using RoRService.Models.DataModels;
using RoRService.Repositories.Helpers;
using System.Collections.Generic;

namespace RoRService.Repositories.Contracts
{
    public interface IOfficesRepository
    {
        IEnumerable<Office> GetOffices();
        Office GetOffice(int officeId);
        IEnumerable<Office> GetOfficesForGame(long gameId);
        Office GetOfficeForGame(long gameId, int officeId);
        RepositoryActionResult<Office> CreateOffice(Office office);
        RepositoryActionResult<Office> UpdateOffice(long gameId, int officeId, Office office);
    }
}
