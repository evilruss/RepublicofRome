namespace RoRService.Models.Contracts
{
    public interface ILeader : ICard, IWarBase
    {
        int Bonus { get; set; }
    }
}