using System;
using System.Collections.Generic;
using System.Text;

namespace Tournament.Core.Dtos
{
    public class GameInfoDto
    {
        public int LastPlayedPlayerId { get; set; }

        public int Number { get; set; }

        public int MinimumNumber { get; set; }
    }
}
