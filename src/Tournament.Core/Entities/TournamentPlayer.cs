using System.Collections.Generic;
using static Tournament.Core.Enum.Enums;

namespace Tournament.Core.Entities
{
    public class TournamentPlayer
    {
        public int Id { get; set; }

        public int TournamentId { get; set; }

        public Tournament Tournament { get; set; }

        public int UserId { get; set; }

        public User User { get; set; }

        public TournamentPlayerStatus Status { get; set; }


    }
}
