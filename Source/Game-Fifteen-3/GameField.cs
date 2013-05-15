// **************************
//
//  Written by Team "Gold"
//  Copyright (c) 2012-2013, Telerik Academy
//
// **************************

using System;
using System.Collections.Generic;
using System.Text;

namespace GameFifteen
{
    /// <summary>
    /// Represents a class that generates the game field 
    /// that will be played on.
    /// </summary>
    public class GameField
    {
        // TODO - use Position for indexing.
        private readonly int TableRows;
        private readonly int TableCols;
        private readonly string[,] Table;
        private const string EMPTY_CELL = " ";

        /// <summary>
        /// Constructor of the game field.
        /// </summary>
        /// <param name="rows">Number of rows.</param>
        /// <param name="cols">Number of columns.</param>
        public GameField(int rows, int cols)
        {
            Table = new string[rows, cols];
            this.TableRows = rows;
            this.TableCols = cols;
        }
        
        /// <summary>
        /// Indexer to provide read/write access to the field.
        /// </summary>
        public string this[int rows, int cols]
        {
            get 
            {
                if (!IsRowIndexInRange(rows))
                {
                    throw new ArgumentOutOfRangeException("rows", "The row index is out of range");
                }

                if (!IsColumnIndexInRange(cols))
                {
                    throw new ArgumentOutOfRangeException("cols", "The column index is out of range");
                }

                return Table[rows, cols];
            }
            set 
            {
                if (!IsRowIndexInRange(rows))
                {
                    throw new ArgumentOutOfRangeException("rows", "The row index is out of range");
                }

                if (!IsColumnIndexInRange(cols))
                {
                    throw new ArgumentOutOfRangeException("cols", "The column index is out of range");
                }

                Table[rows, cols] = value; 
            }
        }

        /// <summary>
        /// Checks wheter the specified row is in the game field
        /// </summary>
        /// <param name="rows">Row idex to check</param>
        /// <returns>True is it is in the field, false otherwise</returns>
        private bool IsRowIndexInRange(int rows)
        {
            if (rows < 0 || rows >= this.TableRows)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Checks wheter the specified column is in the game field
        /// </summary>
        /// <param name="rows">Column idex to check</param>
        /// <returns>True is it is in the field, false otherwise</returns>
        private bool IsColumnIndexInRange(int columns)
        {
            if (columns < 0 || columns >= this.TableCols)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Represents a method to generate a game field 
        /// with random position of the numbers [1, 15] and
        /// one empty cell where the movement of the numbers
        /// will be held.
        /// </summary>
        public void GenerateField()
        {
            Random randomNumbers = new Random();
            List<int> usedNumbers = new List<int>();
            bool isTableFilled = false;
            int randomRow = randomNumbers.Next(TableRows);
            int randomCol = randomNumbers.Next(TableCols);
            this[randomRow, randomCol] = EMPTY_CELL;

            for (int row = 0; row < TableRows; row++)
            {
                for (int col = 0; col < TableCols; col++)
                {
                    isTableFilled = false;
                    do
                    {
                        if (this[row, col] == EMPTY_CELL)
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

        /// <summary>
        /// Represents a method to provide
        /// read access to a number <seealso cref="Position.cs"/>
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public Position GetPosition(string input)
        {
            for (int row = 0; row < TableRows; row++)
            {
                for (int col = 0; col < TableCols; col++)
                {
                    if (this[row, col] == input)
                    {
                        return new Position(row, col);
                    }
                }
            }

            return null;
        }

        /// <summary>
        /// Overrides the default toString
        /// to provide table view of the game field.
        /// </summary>
        /// <returns>The modified string.</returns>
        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendLine("* * * * * * * *");

            for (int row = 0; row < this.TableRows; row++)
            {
                for (int col = 0; col < this.TableCols; col++)
                {
                    if (col == 0)
                    {
                        builder.AppendFormat("* {0,2} ", this[row, col]);
                    }
                    else if (col == 3)
                    {
                        builder.AppendFormat("{0,2} *", this[row, col]);
                        builder.AppendLine();
                    }
                    else
                    {
                        builder.AppendFormat("{0,2} ", this[row, col]);
                    }
                }
            }

            builder.AppendLine("* * * * * * * *\n\n");
            return builder.ToString();
        }
    }
}
