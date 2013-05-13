using System;
using System.Collections.Generic;
using System.Linq;

namespace GameFifteen
{
    public class GameFifteen
    {
<<<<<<< HEAD
        private const int BoardSize = 4;
        private const string WhiteSpace = " ";
        private string[,] matrix;
        private int moveCount;

        public void StartGame()
        {
            this.matrix = new string[BoardSize, BoardSize];
            this.moveCount = 0;

            Console.WriteLine("Welcome to the game \"15\". Please try to arrange the numbers " +
                "sequentially." + Environment.NewLine + "Use 'top' to view the top scoreboard, 'restart' to start a new " +
                "game and 'exit'" + Environment.NewLine + "to quit the game." + Environment.NewLine);

            this.GenerateMatrix();
            this.DrawMatrix();

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
                    Console.WriteLine("Congratulations, you have just putted a new record");
                    Console.Write("Please enter your name : ");
                    TopScores.TopPlayers[i].Score = this.moveCount;
                    TopScores.TopPlayers[i].Name = Console.ReadLine();
                }
            }
        }
=======
        private const int BOARD_SIZE = 4;
        private string[,] matrix;
        private int moveCount;
>>>>>>> d89775755846ff86fdeec24da24512ef49fe1038
        
        private void GenerateMatrix()
        {
            Random random = new Random();
            List<int> usedNumbers = new List<int>();
            bool isFilled = false;
<<<<<<< HEAD
            int row = random.Next(BoardSize);
            int col = random.Next(BoardSize);
            this.matrix[row, col] = WhiteSpace;
=======
            int row = random.Next(BOARD_SIZE);
            int col = random.Next(BOARD_SIZE);
            this.matrix[row, col] = " ";
>>>>>>> d89775755846ff86fdeec24da24512ef49fe1038

            for (int i = 0; i < BoardSize; i++)
            {
                for (int j = 0; j < BoardSize; j++)
                {
                    isFilled = false;

                    do
                    {
<<<<<<< HEAD
                        if (this.matrix[i, j] == WhiteSpace)
=======
                        if (this.matrix[i, j] == " ")
>>>>>>> d89775755846ff86fdeec24da24512ef49fe1038
                        {
                            isFilled = true;
                        }

                        int number = random.Next(1, 16);
                        if (this.matrix[i, j] == null)
                        {
                            if (!usedNumbers.Contains(number))
                            {
                                this.matrix[i, j] = number.ToString();
                                isFilled = true;
                                usedNumbers.Add(number);
                            }
                        }
                    }
                    while (isFilled == false);
                }
            }
        }

        private void DrawMatrix()
        {
            Console.WriteLine("  - - - - - -");

            for (int i = 0; i < BoardSize; i++)
            {
                for (int j = 0; j < BoardSize; j++)
                {
                    if (j == 0)
                    {
                        Console.Write("| {0,2} ", this.matrix[i, j]);
                    }
                    else if (j == 3)
                    {
                        Console.WriteLine("{0,2} |", this.matrix[i, j]);
                    }
                    else
                    {
                        Console.Write("{0,2} ", this.matrix[i, j]);
                    }
                }
            }

            Console.WriteLine("  - - - - - -");
        }

        private bool IsGameFinished()
        {
            int counter = 1;

            for (int i = 0; i < BoardSize; i++)
            {
                for (int j = 0; j < BoardSize; j++)
                {
                    if (this.matrix[i, j] != counter.ToString())
                    {
<<<<<<< HEAD
                        if (counter == 15 && this.matrix[i, j] == WhiteSpace)
=======
                        if (counter == 15 && this.matrix[i, j] == " ")
>>>>>>> d89775755846ff86fdeec24da24512ef49fe1038
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
<<<<<<< HEAD
            for (int i = 0; i < BoardSize; i++)
=======
            for (int i = 0; i < BOARD_SIZE; i++)
>>>>>>> d89775755846ff86fdeec24da24512ef49fe1038
            {
                for (int j = 0; j < BoardSize; j++)
                {
                    if (this.matrix[i, j] == input)
                    {
                        return new Position(i, j);
                    }
                }
            }

            Console.WriteLine("Cheat ! Illegal command ! !");
            return null;
        }

<<<<<<< HEAD
        private void ParseInput(string input)
        {
            bool isMoveValid = false;
=======
        public void StartGame()
        {
            this.matrix = new string[BOARD_SIZE, BOARD_SIZE];
            moveCount = 0;
>>>>>>> d89775755846ff86fdeec24da24512ef49fe1038

            if (input == "exit")
            {
                Console.WriteLine("Good bye !");
            }

<<<<<<< HEAD
            if (input == "restart")
            {
                this.RestartGame();
                return;
            }

            if (input == "top")
            {
                this.PrintTopScores();
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

            Position newPosition = this.CalculatePositionWithDirection(oldPosition, direction);

            if (this.matrix[newPosition.Row, newPosition.Column] == WhiteSpace)
            {
                string itemToMove = this.matrix[oldPosition.Row, oldPosition.Column];
                this.matrix[newPosition.Row, newPosition.Column] = itemToMove;
                this.matrix[oldPosition.Row, oldPosition.Column] = WhiteSpace;
                DrawMatrix();
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
            if (this.matrix[newPosition.Row, newPosition.Column] != WhiteSpace)
            {
                return false;
            }

            return true;
        }

        private Position CalculatePositionWithDirection(Position position, MoveDirection direction)
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
            Console.WriteLine("Here is your new this.matrix, have a good play : " + Environment.NewLine);
            this.matrix = new string[BoardSize, BoardSize];
            this.GenerateMatrix();
            this.DrawMatrix();
            this.moveCount = 0;
        }

        private void PrintTopScores()
        {
            for (int i = 0; i < TopScores.TopScoresSize; i++)
            {
                Console.WriteLine(
                    "Name : {0} , moveCount : {1} ",
=======
            GenerateMatrix();
            DrawMatrix();

            while (!IsGameFinished())
            {
                Console.Write("Enter a number to move : ");
                string input = Console.ReadLine();

                ParseInput(input);
            }

            Console.WriteLine("Your result is {0} moves !", moveCount);

            for (int i = 0; i < TopScores.TOP_SCORES_SIZE; i++)
            {
                if (TopScores.TopPlayers[i].Score > moveCount)
                {
                    Console.WriteLine("Congratulations, you have just putted a new record");
                    Console.Write("Please enter your name : ");
                    TopScores.TopPlayers[i].Score = moveCount;
                    TopScores.TopPlayers[i].Name = Console.ReadLine();
                }
            }
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
                RestartGame();
                return;
            }

            if (input == "top")
            {
                PrintTopScores();
                return;
            }

            Position currentPosition = GetPosition(input);
            if (currentPosition == null)
            {
                return;
            }

            TryMovingInDirection(ref isMoveValid, currentPosition,
                MoveDirection.Up);
            TryMovingInDirection(ref isMoveValid, currentPosition,
                MoveDirection.Down);
            TryMovingInDirection(ref isMoveValid, currentPosition,
                MoveDirection.Left);
            TryMovingInDirection(ref isMoveValid, currentPosition,
                MoveDirection.Right);

            if (!isMoveValid)
            {
                Console.WriteLine("Illegal move!");
            }
        }

        private void MoveInDirection(Position oldPosition, MoveDirection direction)
        {
            Position newPosition = CalculatePositionWithDirection(oldPosition,
                direction);

            if (this.matrix[newPosition.Row, newPosition.Column] == " ")
            {
                string itemToMove = this.matrix[oldPosition.Row, oldPosition.Column];
                this.matrix[newPosition.Row, newPosition.Column] = itemToMove;
                this.matrix[oldPosition.Row, oldPosition.Column] = " ";
                DrawMatrix();
            }
        }

        private void TryMovingInDirection(ref bool isMoveValid, Position currentPosition, 
            MoveDirection direction)
        {
            if(CanMoveInDirection(currentPosition, direction))
            {
                MoveInDirection(currentPosition, direction);
                isMoveValid = true;
                moveCount++;
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

            Position newPosition = CalculatePositionWithDirection(position, direction);
            if (this.matrix[newPosition.Row, newPosition.Column] != " ")
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
            this.matrix = new string[BOARD_SIZE, BOARD_SIZE];
            GenerateMatrix();
            DrawMatrix();
            moveCount = 0;
        }

        private void PrintTopScores()
        {
            for (int i = 0; i < TopScores.TOP_SCORES_SIZE; i++)
            {
                Console.WriteLine("Name : {0} , moveCount : {1} ",
>>>>>>> d89775755846ff86fdeec24da24512ef49fe1038
                    TopScores.TopPlayers[i].Name,
                    TopScores.TopPlayers[i].Score);
            }
        }
    }
}
