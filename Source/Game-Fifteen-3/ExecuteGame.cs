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
    /// Represents a class that 
    /// starts the game.
    /// </summary>
    public class ExecuteGame
    {
        public static void Main(string[] args)
        {
            GameEngine game = new GameEngine();
            game.StartGame();
        }
    }
}
