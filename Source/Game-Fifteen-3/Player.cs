// **************************
//
//  Written by Team "Gold"
//  Copyright (c) 2012-2013, Telerik Academy
//
// **************************

using System;
using System.Linq;

namespace GameFifteen
{
    /// <summary>
    /// Represents a class which provides 
    /// information about the Player 
    /// such as Name and Score.
    /// </summary>
    public class Player
    {
        private string name;
        private int score;
        /// <param name="score">The Score of a player.</param>
        public Player(int score)
        {
            this.Score = score;
        }

        /// <param name="name">The name of the player.</param>
        /// <param name="score">The score of the player.</param>
        public Player(string name, int score)
            : this(score)
        {
            this.Name = name;
        }

        public string Name 
        {
            get
            {
                return this.name;
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException("The name cannot be empty string");
                }

                this.name = value;
            }
        }

        public int Score 
        {
            get
            {
                return this.score;
            }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException("score",
                        "The score should be a positive number");
                }

                this.score = value;
            }
        }
    }
}
