using RoRService.Models.Contracts;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace RoRService.Models.DataModels
{
    public class Player : IPlayer
    {
        public Player()
        {

        }

        public long Id { get; set; }
        public long GameId { get; set; }
        public string FactionName { get; set; }
        public string Name { get; set; }
    }
}