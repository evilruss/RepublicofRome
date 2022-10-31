namespace RoRService.Models.Contracts
{
    public interface IPlayer
    {
        long Id { get; set; }
        string FactionName { get; set; }
        string Name { get; set; }
    }
}
