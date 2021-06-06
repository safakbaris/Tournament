using System.Collections.Generic;

namespace Tournament.Core.Entities
{
    public class Game
    {
        public int Id { get; set; }


        public int TournamentId { get; set; }
        public Tournament Tournament { get; set; }

        public int Player1Id { get; set; }

        public User Player1 { get; set; }

        public int Player2Id { get; set; }

        public User Player2 { get; set; }

        public int Number { get; set; }

        public int MinimumNumber { get; set; }

        public int LastPlayedUserId { get; set; }

        public int WinnerPlayerId { get; set; }

        public ICollection<GameLog> Logs { get; set; }
    }
}
