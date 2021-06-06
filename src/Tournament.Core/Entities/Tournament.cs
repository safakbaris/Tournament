using System;
using System.Collections.Generic;
using static Tournament.Core.Enum.Enums;

namespace Tournament.Core.Entities
{
    public class Tournament
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime StartTime { get; set; }

        public int MinimumPlayers { get; set; }

        public int MaximumPlayers { get; set; }

        public TournamentStatus Status { get; set; } = TournamentStatus.NotStarted;

        public ICollection<TournamentPlayer> Players { get; set; }
        public ICollection<Game> Games { get; set; }
    }
}
