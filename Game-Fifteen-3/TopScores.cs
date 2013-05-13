using System;
using System.Linq;

namespace GameFifteen
{
   public class TopScores
   {
       public static readonly int TopScoresSize = 5;

       // this is the top score table
       public static Player[] TopPlayers = new Player[TopScoresSize];
   }
}
