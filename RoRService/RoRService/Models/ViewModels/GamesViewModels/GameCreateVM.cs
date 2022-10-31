using RoRService.Models.Contracts;
using RoRService.Models.ViewModels.PlayersViewModels;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RoRService.Models.ViewModels.GamesViewModels
{
    public class GameCreateVM : IGame
    {
        public long Id { get; set; }
        [Required]
        [MaxLength(30)]
        [Display(Name = "Game Name")]
        public string Name { get; set; }
        [Required]
        [MaxLength(30)]
        [Display(Name = "Game Owner")]
        public string Owner { get; set; }
        [Required]
        [MaxLength(100)]
        [Display(Name = "Game Description")]
        public string Description { get; set; }
        [Required]
        public string Era { get; set; }
        public NewPlayerVM NewPlayer { get; set; }

        public List<EraListItem> EraList = new List<EraListItem>
        {
            new EraListItem { Era = "Early", DisplayName = "Early Republic" },
            new EraListItem { Era = "Middle", DisplayName = "Middle Republic" },
            new EraListItem { Era = "Late", DisplayName = "Late Republic" }
        };
    }
}