// **************************
//
//  Written by Team "Gold"
//  Copyright (c) 2012-2013, Telerik Academy
//
// **************************
using System;

namespace GameFifteen
{
    /// <summary>
    /// Represents a class that includes methods that
    /// initialize the state of the game, executes user commands
    /// and shows the top scores of the players who have completed the game.
    /// </summary>
    public class GameEngine
    {
        public const int BOARD_SIZE = 4;
        private GameField field;
        private int moveCount;
        private bool isGameRunning = false;
        private bool isGameWon = false;
       
        /// <summary>
        /// Represents a method which starts the game.
        /// </summary>
        public void StartGame()
        {
            this.field = new GameField(BOARD_SIZE, BOARD_SIZE);

            this.moveCount = 0;
            this.field.GenerateField(new RandomFieldGenerator());
            this.isGameRunning = true;
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(Message.WELCOME);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write(field.ToString());

            // While the game is running prompts the user to enter
            // new position to move to.
            while (isGameRunning)
            {
                isGameRunning = !IsGameFinished();
                
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write(Message.MOVE);
                
                string input = Console.ReadLine();
                this.ParseInput(input);

                if (isGameWon)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine(Message.Solved(this.moveCount));
                    this.CheckTopScore();
                    this.StopGame();
                }
            }
            Console.ResetColor();
        }

        /// <summary>
        /// Represents method that checks for a new
        /// <seealso cref="Player.cs"/> high score.
        /// </summary>
        private void CheckTopScore()
        {
            // Complex Expression ? NOTE: ASK TRAINERS
            // int topPlayersSize = TopScore.ScoreListSize < TopScore.TopPlayers.Count 
            //    ? TopScore.ScoreListSize : TopScore.TopPlayers.Count;

            int topPlayersSize;

            if (TopScore.ScoreListSize < TopScore.TopPlayers.Count)
            {
                topPlayersSize = TopScore.ScoreListSize;
            }
            else
            {
                topPlayersSize = TopScore.TopPlayers.Count;
            }

            if (TopScore.TopPlayers.Count == 0)
            {
                this.AddToTopScore(new Player(this.moveCount), 0);
            }
            else
            {
                for (int i = 0; i < topPlayersSize; i++)
                {
                    if (this.moveCount > TopScore.TopPlayers[i].Score)
                    {
                        this.AddToTopScore(new Player(this.moveCount), i);
                    }
                }
            }
        }

        /// <summary>
        /// Represents a method that add <seealso cref="Player.cs"/> score
        /// to the list of <seealso cref="TopScores.cs"/>
        /// </summary>
        /// <param name="player">
        /// An instance of <seealso cref="Player.cs"/> that 
        /// will be used as information to be saved with the score.
        /// </param>
        /// <param name="position">
        /// Position to be added to the List depending
        /// on the score.
        /// </param>
        private void AddToTopScore(Player player, int position)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(Message.CONGRATS);
            Console.ResetColor();

            player.Name = Console.ReadLine();
            TopScore.AddPlayer(player, position);
        }

        /// <summary>
        /// Represents a method that checks if the game state is finished.
        /// </summary>
        /// <returns>
        /// True or False depending on the finished 
        /// state of the game.
        /// </returns>
        private bool IsGameFinished()
        {
            // TODO - rewrite this method using Position.
            // Indicates the proper order of the numbers
            int countElements = 1;

            for (int row = 0; row < BOARD_SIZE; row++)
            {
                for (int col = 0; col < BOARD_SIZE; col++)
                {
                    if (this.field[row, col] != countElements.ToString())
                    {
                        if (countElements == 15 && this.field[row, col] == GameField.EMPTY_CELL)
                        {
                            isGameWon = true;
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }

                    countElements++;
                }
            }

            return true;
        }

        /// <summary>
        /// Represents a method that executes commands 
        /// entered from the console depending on the user input.
        /// </summary>
        /// <param name="input">Console Input Commands</param>
        /// <remarks>
        /// Only commands like "exit", "top", "restart" or
        /// number between [1, 15] is valid.
        /// </remarks>
        private void ParseInput(string input)
        {
            bool isMoveValid = false;

            if (input == Message.EXIT)
            {
                this.StopGame();
                return;
            }

            if (input == Message.RESTART)
            {
                this.RestartGame();
                return;
            }

            if (input == Message.TOP)
            {
                TopScore.PrintTopScores();
                return;
            }

            Position currentPosition = field.GetPosition(input);
            if (currentPosition == null)
            {
                isMoveValid = false;
                return;
            }

            // Atempts to move a number in the direction that has empty cell
            Movement.TryMovingInDirection(ref moveCount, ref isMoveValid, currentPosition, MoveDirection.Up, this.field);
            Movement.TryMovingInDirection(ref moveCount, ref isMoveValid, currentPosition, MoveDirection.Down, this.field);
            Movement.TryMovingInDirection(ref moveCount, ref isMoveValid, currentPosition, MoveDirection.Left, this.field);
            Movement.TryMovingInDirection(ref moveCount, ref isMoveValid, currentPosition, MoveDirection.Right, this.field);

            if (!isMoveValid)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(Message.INVALID_MOVE);
                Console.ResetColor();
            }
        }

        /// <summary>
        /// Represents a method that starts a new game
        /// </summary>
        /// <remarks>The game field will have new numbers.</remarks>
        private void RestartGame()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(Message.GAME_RESTARTED);

            this.moveCount = 0;
            this.field = new GameField(BOARD_SIZE, BOARD_SIZE);
            field.GenerateField(new RandomFieldGenerator());

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write(field.ToString());
            Console.ResetColor();
        }

        /// <summary>
        /// Represents a method that stops the state of the game.
        /// </summary>
        private void StopGame()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(Message.GAME_OVER);

            while (isGameWon)
            {
                Console.Write(Message.NEW_GAME);
                string userInput = Console.ReadLine();

                if (userInput.Equals(Message.CONFIRM, StringComparison.OrdinalIgnoreCase))
                {
                    this.RestartGame();
                    this.isGameRunning = true;
                    this.isGameWon = false;
                    return;
                }
                else if (userInput.Equals(Message.DENY, StringComparison.OrdinalIgnoreCase))
                {
                    this.isGameWon = false;
                }
            }

            this.isGameRunning = false;
            Console.ResetColor();
        }
    }
}