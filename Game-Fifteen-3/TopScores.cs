using System;
using System.Linq;

namespace GameFifteen
{
   public class TopScores
   {
       public static readonly int TopScoresSize = 5;

       // this is the top score table
       public static Player[] TopPlayers = new Player[TopScoresSize];

       private void PrintTopScores()
       {
           for (int i = 0; i < TopScores.TopScoresSize; i++)
           {
               Console.WriteLine("Name : {0} , moveCount : {1} ",
                   TopScores.TopPlayers[i].Name,
                   TopScores.TopPlayers[i].Score);
           }
       }
   }
}
