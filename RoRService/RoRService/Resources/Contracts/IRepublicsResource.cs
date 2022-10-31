using RoRService.Models.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoRService.Resources.Contracts
{
    public interface IRepublicsResource
    {
        Task<Republic> GetRepublic(long gameId);
        Task CreateRepublic(long gameId, Republic republic);
        Task UpdateRepublic(long gameId, Republic republic);
    }
}
