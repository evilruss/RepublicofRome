using RoRService.Models.Contracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RoRService.Models.ViewModels.PlayersViewModels
{
    public class NewPlayerVM : IPlayer
    {
        public NewPlayerVM()
        {

        }

        public long Id { get; set; }
        [Required]
        [MaxLength(30)]
        [Display(Name="Faction Name")]
        public string FactionName { get; set; }
        [Required]
        [MaxLength(30)]
        [Display(Name = "Player")]
        public string Name { get; set; }
    }
}