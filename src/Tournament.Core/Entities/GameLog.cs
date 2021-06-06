using System;
using System.Collections.Generic;
using System.Text;

namespace Tournament.Core.Entities
{
    public class GameLog
    {
        public int Id { get; set; }
        public int GameId { get; set; }
        public Game Game { get; set; }
        public int  UserId { get; set; }
        public int PlayedNumber { get; set; }
    }
}
