namespace RoRService.Models.Contracts
{
    public interface IWarBase
    {
        string WarName { get; set; }
        int[] Disaster { get; set; }
        int[] StandOff { get; set; }
    }
}