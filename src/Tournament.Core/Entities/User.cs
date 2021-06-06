using System.Collections.Generic;
using static Tournament.Core.Enum.Enums;

namespace Tournament.Core.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public UserType UserType { get; set; }

        public ICollection<TournamentPlayer> Tournaments { get; set; }

        public ICollection<Game> Player1Games { get; set; }
        public ICollection<Game> Player2Games { get; set; }



    }
}
