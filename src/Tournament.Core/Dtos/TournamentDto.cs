using System;
using System.Collections.Generic;
using static Tournament.Core.Enum.Enums;

namespace Tournament.Core.Dtos
{
    public class TournamentDto
    {
        public int Id { get; set; }
        public string TournamentName { get; set; }

        public DateTime StartTime { get; set; }

        public int MinimumPlayers { get; set; }

        public int MaximumPlayers { get; set; }

        public string Status { get; set; }

        public int PlayingPlayers { get; set; }
        public int WaitingPlayers { get; set; }


    }
}
