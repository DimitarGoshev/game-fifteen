using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameFifteen
{
    public class RandomFieldGenerator : IFieldGenerator
    {
        public string[,] GenerateField(int rows, int cols)
        {
            string[,] table = new string[rows, cols];
            Random randomNumbers = new Random();
            List<int> usedNumbers = new List<int>();
            bool isTableFilled = false;
            int randomRow = randomNumbers.Next(rows);
            int randomCol = randomNumbers.Next(cols);
            table[randomRow, randomCol] = " ";

            for (int row = 0; row < rows; row++)
            {
                for (int col = 0; col < cols; col++)
                {
                    isTableFilled = false;
                    do
                    {
                        if (table[row, col] == " ")
                        {
                            isTableFilled = true;
                        }

                        int number = randomNumbers.Next(1, 16);
                        if (table[row, col] == null)
                        {
                            if (!usedNumbers.Contains(number))
                            {
                                table[row, col] = number.ToString();
                                isTableFilled = true;
                                usedNumbers.Add(number);
                            }
                        }
                    }
                    while (isTableFilled == false);
                }
            }

            return table;
        }
    }
}
