using System;
using System.Collections.Generic;
using System.Text;

namespace GameFifteen
{
    class GameField
    {
        private int tableRows;
        private int tableCols;
        private string[,] table;
        private const string EmptyCell = " ";

        public GameField(int rows, int cols)
        {
            table = new string[rows, cols];
            this.tableRows = rows;
            this.tableCols = cols;
        }

        public string this[int rows, int cols]
        {
            get { return table[rows, cols]; }
            set { table[rows, cols] = value; }
        }

        public void GenerateField()
        {
            Random randomNumbers = new Random();
            List<int> usedNumbers = new List<int>();
            bool isTableFilled = false;
            int randomRow = randomNumbers.Next(tableRows);
            int randomCol = randomNumbers.Next(tableCols);
            this[randomRow, randomCol] = EmptyCell;

            for (int row = 0; row < tableRows; row++)
            {
                for (int col = 0; col < tableCols; col++)
                {
                    isTableFilled = false;
                    do
                    {
                        if (this[row, col] == EmptyCell)
                        {
                            isTableFilled = true;
                        }

                        int number = randomNumbers.Next(1, 16);
                        if (this[row, col] == null)
                        {
                            if (!usedNumbers.Contains(number))
                            {
                                this[row, col] = number.ToString();
                                isTableFilled = true;
                                usedNumbers.Add(number);
                            }
                        }
                    }
                    while (isTableFilled == false);
                }
            }
        }

        public Position GetPosition(string input)
        {
            for (int row = 0; row < tableRows; row++)
            {
                for (int col = 0; col < tableCols; col++)
                {
                    if (this[row, col] == input)
                    {
                        return new Position(row, col);
                    }
                }
            }

            return null;
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendLine("  - - - - - -");

            for (int row = 0; row < this.tableRows; row++)
            {
                for (int col = 0; col < this.tableCols; col++)
                {
                    if (col == 0)
                    {
                        builder.AppendFormat("| {0,2} ", this[row, col]);
                    }
                    else if (col == 3)
                    {
                        builder.AppendFormat("{0,2} |", this[row, col]);
                        builder.AppendLine();
                    }
                    else
                    {
                        builder.AppendFormat("{0,2} ", this[row, col]);
                    }
                }
            }

            builder.AppendLine("  - - - - - -");
            return builder.ToString();
        }
    }
}
