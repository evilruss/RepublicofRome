namespace RoRService.Models.Contracts
{
    public interface IConcession : ICard, ISpecialRules
    {
        int Income { get; set; }
    }
}