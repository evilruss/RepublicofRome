namespace RoRService.Models.Contracts
{
    public interface IWar : ICard, IWarBase, ISpecialRules
    {
        int Legions { get; set; }
        int FleetSupport { get; set; }
        int Fleets { get; set; }
        int Talents { get; set; }
    }
}