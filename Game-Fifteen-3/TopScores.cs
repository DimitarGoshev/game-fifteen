using System;
using System.Linq;
using System.Collections.Generic;

namespace GameFifteen
{
    public static class TopScores
    {
        public static readonly int SCORE_LIST_SIZE = 5;
        public static List<Player> TopPlayers = new List<Player>();

        public static void AddPlayer(Player player, int position)
        {
            TopPlayers.Insert(position, player);
        }

        public static void PrintTopScores()
        {
            for (int i = 0; i < TopScores.SCORE_LIST_SIZE; i++)
            {
                Console.WriteLine("Name : {0}, Solved in : {1} moves!",
                TopScores.TopPlayers[i].Name,
                TopScores.TopPlayers[i].Score);
            }
        }
    }
}
