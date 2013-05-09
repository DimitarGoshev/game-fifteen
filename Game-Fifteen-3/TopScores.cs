using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameFifteen
{
   public class TopScores
   {
       public static readonly int TOP_SCORES_SIZE = 5;

       //this is the top score table
       public static Player[] TopPlayers = new Player[TOP_SCORES_SIZE];
   }
}
