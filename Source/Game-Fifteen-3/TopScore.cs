using System;
using System.Linq;
using System.Collections.Generic;

namespace GameFifteen
{
    /// <summary>
    /// Represents a class that provides information
    /// by Score,
    /// about Players who have finished the game successfully.
    /// </summary>
    public static class TopScore
    {
        public static readonly int ScoreListSize = 5;
        public static List<Player> TopPlayers = new List<Player>();

        /// <summary>
        /// Represents a method that 
        /// adds a <seealso cref="Player.cs"/> name to the list of Top Scores.
        /// </summary>
        /// <param name="player">The player information that will be written to the lsit</param>
        /// <param name="position">The position of the player to be added at.</param>
        public static void AddPlayer(Player player, int position)
        {
            TopPlayers.Insert(position, player);
        }    

        /// <summary>
        /// Represents a method that prints on the console
        /// the list of Top Scores if it is not empty.
        /// </summary>
        public static void PrintTopScores()
        {
            int countPlayers = 0;

            if (TopPlayers.Count == 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Top score list is empty.");
                Console.ResetColor();
            }
            else
            {
                for (int i = 0; i < TopPlayers.Count; i++)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Name : {0}, Solved in : {1} moves!",
                    TopPlayers[i].Name,
                    TopPlayers[i].Score);
                    Console.ResetColor();
                    if (countPlayers == ScoreListSize)
                    {
                        break;
                    }

                    countPlayers++;
                }
            }
        }
    }
}
