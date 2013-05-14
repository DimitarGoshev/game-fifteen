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
        private GameField field;
        private const string EmptyCell = " ";
        private int moveCount;
        private const int BOARD_SIZE = 4;
        private bool isGameRunning = false;
        private bool isGameWon = false;

        /// <summary>
        /// Represents a method which starts the game.
        /// </summary>
        public void StartGame()
        {
            this.field = new GameField(BOARD_SIZE, BOARD_SIZE);
            this.moveCount = 0;
            this.field.GenerateField();
            this.isGameRunning = true;

            Console.WriteLine("Welcome to the game \"15\". Please try to arrange the numbers " +
                "sequentially .\nUse 'top' to view the top scoreboard, 'restart' to start a new " +
                "game and 'exit' \nto quit the game.\n\n\n");

            Console.Write(field.ToString());

            // while the game is running prompts the user to enter
            // new position to move to.
            while (isGameRunning)
            {
                isGameRunning = !IsGameFinished();
                Console.Write("Enter a number to move [1..15]: ");
                string input = Console.ReadLine();
                this.ParseInput(input);
            }

            if (isGameWon)
            {
                Console.WriteLine("You solved the game in {0} moves!", this.moveCount);
                this.CheckTopScore();
            }
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
            Console.WriteLine("Congratulations, you have just set a new record!");
            Console.Write("Please enter your name : ");
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
            // counter indicates the proper order of the numbers
            int counter = 1;

            for (int row = 0; row < BOARD_SIZE; row++)
            {
                for (int col = 0; col < BOARD_SIZE; col++)
                {
                    if (this.field[row, col] != counter.ToString())
                    {
                        if (counter == 15 && this.field[row, col] == EmptyCell)
                        {
                            isGameWon = true;
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }

                    counter++;
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

            if (input == "exit")
            {
                this.StopGame();
            }

            if (input == "restart")
            {
                this.RestartGame();
                return;
            }

            if (input == "top")
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

            // atempts to move a number in the direction that has empty cell
            this.TryMovingInDirection(ref isMoveValid, currentPosition, MoveDirection.Up);
            this.TryMovingInDirection(ref isMoveValid, currentPosition, MoveDirection.Down);
            this.TryMovingInDirection(ref isMoveValid, currentPosition, MoveDirection.Left);
            this.TryMovingInDirection(ref isMoveValid, currentPosition, MoveDirection.Right);

            if (!isMoveValid)
            {
                Console.WriteLine("That number can't be moved!");
            }
        }

        /// <summary>
        /// Represents a method that moves the number in the available <seealso cref="MoveDirection.cs"/>.
        /// </summary>
        /// <param name="oldPosition">The position that the number was originally set to.</param>
        /// <param name="direction">The direction of the available movement.</param>
        private void MoveInDirection(Position oldPosition, MoveDirection direction)
        {
            Position newPosition = this.CalculatePositionWithDirection(oldPosition,
                direction);

            if (this.field[newPosition.Row, newPosition.Column] == EmptyCell)
            {
                string itemToMove = this.field[oldPosition.Row, oldPosition.Column];
                this.field[newPosition.Row, newPosition.Column] = itemToMove;
                this.field[oldPosition.Row, oldPosition.Column] = EmptyCell;
                Console.Write(this.field.ToString());
            }
        }

        /// <summary>
        /// Represents a method that attempts to move the number in proper
        /// <seealso cref="MoveDirecction.cs"/>
        /// </summary>
        /// <param name="isMoveValid">If the movement is valid.</param>
        /// <param name="currentPosition">The current position of the number..</param>
        /// <param name="direction">The direction to attempt to move to.</param>
        private void TryMovingInDirection(ref bool isMoveValid, Position currentPosition, MoveDirection direction)
        {
            if (this.CanMoveInDirection(currentPosition, direction))
            {
                this.MoveInDirection(currentPosition, direction);
                isMoveValid = true;
                this.moveCount++;
            }
        }

        /// <summary>
        /// Represents a method that validates the state of the number movement.
        /// </summary>
        /// <param name="currentPosition">The current position of the number..</param>
        /// <param name="direction">The direction to attempt to move to.</param>
        /// <returns>True or False depending if the number can be moved
        /// in the given <seealso cref="MoveDirection.cs"/></returns>
        private bool CanMoveInDirection(Position position, MoveDirection direction)
        {
            if (direction == MoveDirection.Up &&
                position.Row <= 0)
            {
                return false;
            }

            if (direction == MoveDirection.Down &&
                position.Row >= BOARD_SIZE - 1)
            {
                return false;
            }

            if (direction == MoveDirection.Left &&
                position.Column <= 0)
            {
                return false;
            }

            if (direction == MoveDirection.Right &&
                position.Column >= BOARD_SIZE - 1)
            {
                return false;
            }

            Position newPosition = this.CalculatePositionWithDirection(position, direction);

            if (this.field[newPosition.Row, newPosition.Column] != EmptyCell)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Represents a method that gives the moved number new
        /// coordinates in the GameField
        /// </summary>
        /// <param name="position">The Old Position of the number.</param>
        /// <param name="direction">The direction to move to.</param>
        /// <returns>The new position of the number.</returns>
        private Position CalculatePositionWithDirection(Position position,
            MoveDirection direction)
        {
            Position newPosition = (Position)position.Clone();

            switch (direction)
            {
                case MoveDirection.Down:
                    newPosition.Row++;
                    break;
                case MoveDirection.Left:
                    newPosition.Column--;
                    break;
                case MoveDirection.Right:
                    newPosition.Column++;
                    break;
                case MoveDirection.Up:
                    newPosition.Row--;
                    break;
            }

            return newPosition;
        }

        /// <summary>
        /// Represents a method that starts a new game
        /// </summary>
        /// <remarks>The game field will have new numbers.</remarks>
        private void RestartGame()
        {
            Console.WriteLine("Game restarted. Generating new table...");
            this.field = new GameField(BOARD_SIZE, BOARD_SIZE);
            field.GenerateField();
            Console.WriteLine("Done!");
            Console.Write(field.ToString());
            this.moveCount = 0;
        }

        /// <summary>
        /// Represents a method that stops the state of the game.
        /// </summary>
        private void StopGame()
        {
            Console.WriteLine("Game Over!");

            if (isGameWon)
            {
                Console.Write("Start Another Game? (Y/N): ");
                while (isGameRunning)
                {
                    string userInput = Console.ReadLine();

                    if (userInput.Equals("Y"))
                    {
                        this.RestartGame();
                        break;
                    }
                    else if (userInput.Equals("N"))
                    {
                        this.isGameRunning = false;
                    }
                }
            }
            else
            {
                this.isGameRunning = false;
            }
        }
    }
}
