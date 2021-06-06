using System;
using System.ComponentModel.DataAnnotations;
using Tourament.Web.Helpers;

namespace Tourament.Web.Models
{
    public class CreateTournamentModel
    {
        [Required(AllowEmptyStrings = false)]
        public string Name { get; set; }

        [Required]
        [CheckDateRange]
        public DateTime StartTime { get; set; }

        [Range(2, int.MaxValue)]
        public int MinimumPlayers { get; set; }

        [Range(2, int.MaxValue)]
        public int MaximumPlayers { get; set; }
    }
}