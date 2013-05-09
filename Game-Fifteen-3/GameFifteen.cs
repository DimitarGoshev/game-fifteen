using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameFifteen
{
    public class GameFifteen
    {
        private const int BOARD_SIZE = 4;
        
        private static void GenerateMatrix(string[,] matrix)
        {
            Random random = new Random();
            List<int> usedNumbers = new List<int>();
            bool isFilled = false;
            int row = random.Next(BOARD_SIZE);
            int col = random.Next(BOARD_SIZE);
            matrix[row, col] = " ";

            for (int i = 0; i < BOARD_SIZE; i++)
            {
                for (int j = 0; j < BOARD_SIZE; j++)
                {
                    isFilled = false;

                    do
                    {
                        if (matrix[i, j] == " ")
                        {
                            isFilled = true;
                        }

                        int number = random.Next(1, 16);
                        if (matrix[i, j] == null)
                        {
                            if (!usedNumbers.Contains(number))
                            {
                                matrix[i, j] = number.ToString();
                                isFilled = true;
                                usedNumbers.Add(number);
                            }
                        }
                    }
                    while (isFilled == false);
                }
            }
        }

        private static void DrawMatrix(string[,] matrix)
        {
            Console.WriteLine("  - - - - - -");

            for (int i = 0; i < BOARD_SIZE; i++)
            {
                for (int j = 0; j < BOARD_SIZE; j++)
                {
                    if (j == 0)
                    {
                        Console.Write("| {0,2} ", matrix[i, j]);
                    }
                    else if (j == 3)
                    {
                        Console.WriteLine("{0,2} |", matrix[i, j]);
                    }
                    else
                    {
                        Console.Write("{0,2} ", matrix[i, j]);
                    }
                }
            }

            Console.WriteLine("  - - - - - -");
        }

        private static bool IsGameFinished(string[,] matrix)
        {
            int counter = 1;

            for (int i = 0; i < BOARD_SIZE; i++)
            {
                for (int j = 0; j < BOARD_SIZE; j++)
                {
                    if (matrix[i, j] != counter.ToString())
                    {
                        if (counter == 15 && matrix[i, j] == " ")
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

        private static Position GetFirstEmptyPosition(string[,] matrix)
        {
            Position result = new Position(-1, -1);
            for (int i = 0; i < BOARD_SIZE; i++)
            {
                for (int j = 0; j < BOARD_SIZE; j++)
                {
                    if (matrix[i, j] == " ")
                    {
                        result = new Position(i, j);
                    }
                }
            }

            return result;
        }

        private static void ChangeAndDraw(string[,] matrica, int rowToChange,
            int columnToChange, int row, int column, string input)
        {
            matrica[rowToChange, columnToChange] = input;
            matrica[row, column] = " ";
            DrawMatrix(matrica);
        }

        private static Position GetPosition(string[,] matrix, string input)
        {
            for (int i = 0; i < BOARD_SIZE; i++)
            {
                for (int j = 0; j < BOARD_SIZE; j++)
                {
                    if (matrix[i, j] == input)
                    {
                        return new Position(i, j);
                    }
                }
            }

            Console.WriteLine("Cheat ! Illegal command ! !");
            return null;
        }

        static void Main(string[] args)
        {
            string[,] matrix = new string[BOARD_SIZE, BOARD_SIZE];
            int moveCount = 0;

            Console.WriteLine("Welcome to the game \"15\". Please try to arrange the numbers " +
                "sequentially .\nUse 'top' to view the top scoreboard, 'restart' to start a new " +
                "game and 'exit' \nto quit the game.\n\n\n");

            GenerateMatrix(matrix);
            DrawMatrix(matrix);

            while (!IsGameFinished(matrix))
            {
                Console.Write("Enter a number to move : ");
                string input = Console.ReadLine();
                bool isEmpty = false;

                if (input == "exit")
                {
                    Console.WriteLine("Good bye !");
                }

                if (input == "restart")
                {
                    Console.WriteLine("Here is your new matrix, have a good play : \n\n\n");
                    matrix = new string[BOARD_SIZE, BOARD_SIZE];
                    GenerateMatrix(matrix);
                    DrawMatrix(matrix);
                    moveCount = 0;
                    continue;
                }

                if (input == "top")
                {
                    for (int i = 0; i < TopScores.TOP_SCORES_SIZE; i++)
                    {
                        Console.WriteLine("Name : {0} , moveCount : {1} ", 
                            TopScores.TopPlayers[i].Name, 
                            TopScores.TopPlayers[i].Score);
                    }

                    continue;
                }

                if (GetPosition(matrix, input) == null)
                {
                    continue;
                }

                Position currentPosition = GetPosition(matrix, input);

                for (int i = 0; i < BOARD_SIZE; i++)
                {
                    if (i == 0)
                    {
                        if (currentPosition.Row - 1 < 0)
                        {
                            continue;
                        }
                        else
                        {
                            if (matrix[currentPosition.Row - 1, currentPosition.Column] == " ")
                            {
                                ChangeAndDraw(matrix, currentPosition.Row - 1, 
                                    currentPosition.Column, currentPosition.Row, 
                                    currentPosition.Column, input);

                                isEmpty = true;
                                moveCount++;
                            }
                        }
                    }

                    if (i == 1)
                    {
                        if (currentPosition.Row + 1 > 3)
                        {
                            continue;
                        }
                        else
                        {
                            if (matrix[currentPosition.Row + 1, currentPosition.Column] == " ")
                            {
                                ChangeAndDraw(matrix, currentPosition.Row + 1, 
                                    currentPosition.Column, currentPosition.Row, 
                                    currentPosition.Column, input);

                                isEmpty = true;
                                moveCount++;
                            }
                        }
                    }

                    if (i == 2)
                    {
                        if (currentPosition.Column - 1 < 0)
                        {
                            continue;
                        }
                        else
                        {
                            if (matrix[currentPosition.Row, currentPosition.Column - 1] == " ")
                            {
                                ChangeAndDraw(matrix, currentPosition.Row, 
                                    currentPosition.Column - 1, currentPosition.Row, 
                                    currentPosition.Column, input);

                                isEmpty = true;
                                moveCount++;
                            }
                        }
                    }

                    if (i == 3)
                    {
                        if (currentPosition.Column + 1 > 3)
                        {
                            continue;
                        }
                        else
                        {
                            if (matrix[currentPosition.Row, currentPosition.Column + 1] == " ")
                            {
                                ChangeAndDraw(matrix, currentPosition.Row, 
                                    currentPosition.Column + 1, 
                                    currentPosition.Row, currentPosition.Column, 
                                    input);

                                isEmpty = true;
                                moveCount++;
                            }
                        }
                    }
                }

                if (!isEmpty)
                {
                    Console.WriteLine("Cheat ! Illegal command ! !");
                }
            }

            Console.WriteLine("Your result is {0} moveCount !", moveCount);

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
    }
}








