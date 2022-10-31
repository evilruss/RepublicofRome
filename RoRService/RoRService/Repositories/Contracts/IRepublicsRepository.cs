using RoRService.Models.DataModels;
using RoRService.Repositories.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoRService.Repositories.Contracts
{
    public interface IRepublicsRepository
    {
        Republic GetRepublic(long GameId);
        RepositoryActionResult<Republic> CreateRepublic(long GameId, Republic republic);
        RepositoryActionResult<Republic> UpdateRepublic(long GameId, Republic republic);
    }
}
