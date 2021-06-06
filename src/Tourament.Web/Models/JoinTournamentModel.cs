using System.ComponentModel.DataAnnotations;

namespace Tourament.Web.Models
{
    public class JoinTournamentModel
    {
        [Required]
        public int TournamentId { get; set; }

    }
}
