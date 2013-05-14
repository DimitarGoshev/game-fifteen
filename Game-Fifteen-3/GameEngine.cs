using System;

namespace GameFifteen
{
    class GameEngine
    {
        private GameField field;
        private const string EmptyCell = " ";
        private int moveCount;
        private const int BoardSize = 4;
        private bool gameRunning = false;
        private bool gameWon = false;

        public void StartGame()
        {
            field = new GameField(BoardSize, BoardSize);
            this.moveCount = 0;
            field.GenerateField();
            this.gameRunning = true;

            Console.WriteLine("Welcome to the game \"15\". Please try to arrange the numbers " +
                "sequentially .\nUse 'top' to view the top scoreboard, 'restart' to start a new " +
                "game and 'exit' \nto quit the game.\n\n\n");

            Console.Write(field.ToString());

            while (gameRunning)
            {
                gameRunning = !IsGameFinished();
                Console.Write("Enter a number to move [1..15]: ");
                string input = Console.ReadLine();
                this.ParseInput(input);
            }

            if (gameWon)
            {
                Console.WriteLine("You solved the game in {0} moves!", this.moveCount);
                this.CheckTopScore();
            }
        }

        private void CheckTopScore()
        {
            int topPlayersSize = TopScores.SCORE_LIST_SIZE < TopScores.TopPlayers.Count ? TopScores.SCORE_LIST_SIZE : TopScores.TopPlayers.Count;

            if (TopScores.TopPlayers.Count == 0)
            {
                this.AddToTopScore(new Player(this.moveCount), 0);
            }
            else
            {
                for (int i = 0; i < topPlayersSize; i++)
                {
                    if (this.moveCount > TopScores.TopPlayers[i].Score)
                    {
                        this.AddToTopScore(new Player(this.moveCount), i);
                    }
                }
            }
        }
        private void AddToTopScore(Player player, int position)
        {
            Console.WriteLine("Congratulations, you have just set a new record!");
            Console.Write("Please enter your name : ");
            player.Name = Console.ReadLine();
        }

        private bool IsGameFinished()
        {
            int counter = 1;

            for (int row = 0; row < BoardSize; row++)
            {
                for (int col = 0; col < BoardSize; col++)
                {
                    if (this.field[row, col] != counter.ToString())
                    {
                        if (counter == 15 && this.field[row, col] == EmptyCell)
                        {
                            gameWon = true;
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
                TopScores.PrintTopScores();
                return;
            }

            Position currentPosition = field.GetPosition(input);
            if (currentPosition == null)
            {
                isMoveValid = false;
                return;
            }

            this.TryMovingInDirection(ref isMoveValid, currentPosition, MoveDirection.Up);
            this.TryMovingInDirection(ref isMoveValid, currentPosition, MoveDirection.Down);
            this.TryMovingInDirection(ref isMoveValid, currentPosition, MoveDirection.Left);
            this.TryMovingInDirection(ref isMoveValid, currentPosition, MoveDirection.Right);

            if (!isMoveValid)
            {
                Console.WriteLine("That number can't be moved!");
            }
        }

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

        private void TryMovingInDirection(ref bool isMoveValid, Position currentPosition, MoveDirection direction)
        {
            if (this.CanMoveInDirection(currentPosition, direction))
            {
                this.MoveInDirection(currentPosition, direction);
                isMoveValid = true;
                this.moveCount++;
            }
        }

        private bool CanMoveInDirection(Position position, MoveDirection direction)
        {
            if (direction == MoveDirection.Up &&
                position.Row <= 0)
            {
                return false;
            }

            if (direction == MoveDirection.Down &&
                position.Row >= BoardSize - 1)
            {
                return false;
            }

            if (direction == MoveDirection.Left &&
                position.Column <= 0)
            {
                return false;
            }

            if (direction == MoveDirection.Right &&
                position.Column >= BoardSize - 1)
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

        private void RestartGame()
        {
            Console.WriteLine("Game restarted. Generating new table...");
            this.field = new GameField(BoardSize, BoardSize);
            field.GenerateField();
            Console.WriteLine("Done!");
            Console.Write(field.ToString());
            this.moveCount = 0;
        }

        private void StopGame()
        {
            Console.WriteLine("Game Over!");

            if (gameWon)
            {
                Console.Write("Start Another Game? (Y/N): ");
                while (gameRunning)
                {
                    string userInput = Console.ReadLine();

                    if (userInput.Equals("Y"))
                    {
                        this.RestartGame();
                        break;
                    }
                    else if (userInput.Equals("N"))
                    {
                        this.gameRunning = false;
                    }
                }
            }
            else
            {
                this.gameRunning = false;
            }
        }
    }
}
