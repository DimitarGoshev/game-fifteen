using System.Text;

namespace GameFifteen
{
    /// <summary>
    /// Represents a class for storing user messages.
    /// </summary>
    class Message
    {
        public const string WELCOME = "Welcome to the Game \"15\".\n"
                                    + "Please try to arrange the numbers sequentially.\n"
                                    + "You can use the following commands:\n"
                                    + "'top' to view the top scoreboard,\n"
                                    + "'restart' to start a new game,\n"
                                    + "'exit' to quit the game.\n\n";

        public const string MOVE = @"Enter a number to move [1..15]: ";

        public const string CONGRATS = "Congratulations, you have just set a new record!\n"
                                        + "Please enter your name : ";

        public const string EXIT = @"exit";
        public const string RESTART = @"restart";
        public const string TOP = @"top";
        public const string INVALID_MOVE = @"That number can't be moved!";
        public const string GAME_RESTARTED = @"Game restarted. Generating new table...";
        public const string GAME_OVER = @"Game Over!";
        public const string NEW_GAME = @"Start Another Game? (Y/N): ";
        public const string CONFIRM = "Y";
        public const string DENY = "N";
        public const string EMPTY_TOP_SCORE = @"Top score list is empty.";

        public static string Solved(int moves)
        {
            return "You solved the game in" + moves + "moves!";
        }
    }
}
