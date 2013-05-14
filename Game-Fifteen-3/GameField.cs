using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameFifteen
{
    class GameField
    {
        public int Rows;
        public int Cols;
        private string[,] matrix;

        //Create field.
        public GameField(int rows, int cols)
        {
            matrix = new string[rows, cols];
            Rows = rows;
            Cols = cols;
        }

        // Get/Set
        public string this[int rows, int cols]
        {
            get { return matrix[rows, cols]; }
            set { matrix[rows, cols] = value; }
        }

        public GameField GenerateField()
        {
            Random random = new Random();
            List<int> usedNumbers = new List<int>();
            bool isFilled = false;
            int row = random.Next(BoardSize);
            int col = random.Next(BoardSize);
            this.matrix[row, col] = EmptyCell;

            for (int i = 0; i < BoardSize; i++)
            {
                for (int j = 0; j < BoardSize; j++)
                {
                    isFilled = false;

                    do
                    {
                        if (this.matrix[i, j] == EmptyCell)
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

        //Overload ToString()
        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();

            for (int rows = 0; rows < this.Rows; rows++)
            {
                for (int cols = 0; cols < this.Cols; cols++)
                {
                    builder.AppendFormat("{0,4}", this[rows, cols]);
                }
                builder.AppendLine();
            }

            return builder.ToString();
        }
    }
}
