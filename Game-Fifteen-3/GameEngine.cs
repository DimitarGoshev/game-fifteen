using System;
using System.Linq;

namespace GameFifteen
{
    class GameEngine
    {
        private GameField field;
        private const string EmptyCell = " ";
        private int moveCount;
        private const int BoardSize = 4;

        public void StartGame()
        {

            field = new GameField(BoardSize, BoardSize);
            this.moveCount = 0;

            Console.WriteLine("Welcome to the game \"15\". Please try to arrange the numbers " +
                "sequentially .\nUse 'top' to view the top scoreboard, 'restart' to start a new " +
                "game and 'exit' \nto quit the game.\n\n\n");

            field.GenerateField();
            Console.Write(field.ToString());

            while (!this.IsGameFinished())
            {
                Console.Write("Enter a number to move : ");
                string input = Console.ReadLine();

                this.ParseInput(input);
            }

            Console.WriteLine("Your result is {0} moves !", this.moveCount);

            for (int i = 0; i < TopScores.TopScoresSize; i++)
            {
                if (TopScores.TopPlayers[i].Score > this.moveCount)
                {
                    Console.WriteLine("Congratulations, you have just set a new record");
                    Console.Write("Please enter your name : ");
                    TopScores.TopPlayers[i].Score = this.moveCount;
                    TopScores.TopPlayers[i].Name = Console.ReadLine();
                }
            }
        }

        private bool IsGameFinished()
        {
            int counter = 1;

            for (int i = 0; i < BoardSize; i++)
            {
                for (int j = 0; j < BoardSize; j++)
                {
                    if (this.field[i, j] != counter.ToString())
                    {
                        if (counter == 15 && this.field[i, j] == EmptyCell)
                        {
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

        private Position GetPosition(string input)
        {
            for (int i = 0; i < BoardSize; i++)
            {
                for (int j = 0; j < BoardSize; j++)
                {
                    if (this.field[i, j] == input)
                    {
                        return new Position(i, j);
                    }
                }
            }

            Console.WriteLine("Cheat ! Illegal command ! !");
            return null;
        }



        private void ParseInput(string input)
        {
            bool isMoveValid = false;

            if (input == "exit")
            {
                Console.WriteLine("Good bye !");
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

            Position currentPosition = this.GetPosition(input);
            if (currentPosition == null)
            {
                return;
            }

            this.TryMovingInDirection(ref isMoveValid, currentPosition, MoveDirection.Up);
            this.TryMovingInDirection(ref isMoveValid, currentPosition, MoveDirection.Down);
            this.TryMovingInDirection(ref isMoveValid, currentPosition, MoveDirection.Left);
            this.TryMovingInDirection(ref isMoveValid, currentPosition, MoveDirection.Right);

            if (!isMoveValid)
            {
                Console.WriteLine("Illegal move!");
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
            Console.WriteLine("Here is your new this.matrix, have a good play : \n\n\n");
            this.field = new GameField(BoardSize, BoardSize);
            field.GenerateField();
            Console.Write(field.ToString());
            this.moveCount = 0;
        }
    }
}