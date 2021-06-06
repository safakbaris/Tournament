namespace Tournament.Core.Enum
{
    public class Enums
    {
        public enum UserType
        {
            Player,
            Admin
        }

        public enum TournamentStatus
        {
            NotStarted,
            Started,
            Cancelled,
            Finished
        }

        public enum TournamentPlayerStatus
        {
            Waiting,
            Playing,
            Eliminated,
            Winner
        }

    }
}
