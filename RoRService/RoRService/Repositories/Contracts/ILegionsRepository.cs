using RoRService.Models.DataModels;
using RoRService.Repositories.Helpers;
using System.Collections.Generic;

namespace RoRService.Repositories.Contracts
{
    public interface ILegionsRepository
    {
        IEnumerable<Legion> GetLegions(long gameId);
        RepositoryActionResult<List<Legion>> CreateLegions(List<Legion> newLegionsList);
    }
}
