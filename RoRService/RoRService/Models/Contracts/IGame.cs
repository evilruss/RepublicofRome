namespace RoRService.Models.Contracts
{
    interface IGame
    {
        long Id { get; set; }
        string Name { get; set; }
        string Owner { get; set; }
        string Description { get; set; }
        string Era { get; set; }
    }
}
