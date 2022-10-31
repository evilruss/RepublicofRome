using RoRService.Models.DataModels;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace RoRService.Resources.Contracts
{
    public interface ILegionsResource
    {
        Task<List<Legion>> GetLegions(long gameId);
        Task<HttpResponseMessage> CreateLegions(long gameId, List<Legion> legionList);
    }
}
