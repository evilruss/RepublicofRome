using System.Collections.Generic;

namespace RoRService.Models.Contracts
{
    public interface ISpecialRules
    {
        List<string> SpecialRules { get; set; }
    }
}