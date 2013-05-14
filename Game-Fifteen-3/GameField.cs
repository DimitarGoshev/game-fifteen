using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameFifteen
{
    class GameField
    {
        private int rows;
        private int cols;
        private string[,] matrix;
        private const string EmptyCell = " ";

        //Create field.
        public GameField(int rows, int cols)
        {
            matrix = new string[rows, cols];
            this.rows = rows;
            this.cols = cols;
        }

        // Get/Set
        public string this[int rows, int cols]
        {
            get { return matrix[rows, cols]; }
            set { matrix[rows, cols] = value; }
        }

        public void GenerateField()
        {
            Random random = new Random();
            List<int> usedNumbers = new List<int>();
            bool isFilled = false;
            int row = random.Next(rows);
            int col = random.Next(cols);
            this[row, col] = EmptyCell;

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    isFilled = false;
                    do
                    {
                        if (this[i, j] == EmptyCell)
                        {
                            isFilled = true;
                        }

                        int number = random.Next(1, 16);
                        if (this[i, j] == null)
                        {
                            if (!usedNumbers.Contains(number))
                            {
                                this[i, j] = number.ToString();
                                isFilled = true;
                                usedNumbers.Add(number);
                            }
                        }
                    }
                    while (isFilled == false);
                }
            }
        }

        //Overload ToString()
        //public override string ToString()
        //{
        //    StringBuilder builder = new StringBuilder();

        //    for (int rows = 0; rows < this.rows; rows++)
        //    {
        //        for (int cols = 0; cols < this.cols; cols++)
        //        {
        //            builder.AppendFormat("{0,4}", this[rows, cols]);
        //        }
        //        builder.AppendLine();
        //    }

        //    return builder.ToString();
        //}

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendLine("  - - - - - -");

            for (int i = 0; i < this.rows; i++)
            {
                for (int j = 0; j < this.cols; j++)
                {
                    if (j == 0)
                    {
                        builder.AppendFormat("| {0,2} ", this[i, j]);
                    }
                    else if (j == 3)
                    {
                        builder.AppendFormat("{0,2} |", this[i, j]);
                        builder.AppendLine();
                    }
                    else
                    {
                        builder.AppendFormat("{0,2} ", this[i, j]);
                    }
                }
            }

            builder.AppendLine("  - - - - - -");
            return builder.ToString();
        }
    }
}
