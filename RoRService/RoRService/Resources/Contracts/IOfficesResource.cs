using RoRService.Models.DataModels;
using RoRService.Models.DataModels.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoRService.Resources.Contracts
{
    public interface IOfficesResource
    {
        Task<Office> GetOffice(OfficeTitle officeTitle);
        Task CreateOffices(long gameId);
        Task UpdateOffice(long gameId, Office office);
    }
}
