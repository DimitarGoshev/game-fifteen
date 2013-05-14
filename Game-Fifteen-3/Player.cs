using System;
using System.Linq;

namespace GameFifteen
{
    public class Player
    {
        public Player(int score)
        {
            this.Score = score;
        }

        public Player(string name, int score)
            : this(score)
        {
            this.Name = name;
        }

        public string Name { get; set; }

        public int Score { get; set; }
    }
}
