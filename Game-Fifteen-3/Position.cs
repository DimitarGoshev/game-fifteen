using System;

namespace GameFifteen
{
    /// <summary>
    /// Represents a class which provides 
    /// read/write information about Position 
    /// of a given cell coordinates.
    /// </summary>
    public class Position : ICloneable
    {

        /// <param name="row">Number of the row</param>
        /// <param name="column">Number of the column.</param>
        public Position(int row, int column)
        {
            this.Row = row;
            this.Column = column;
        }

        public int Row { get; set; }

        public int Column { get; set; }

        /// <summary>
        /// Provides a method that 
        /// returns a value of the current position.
        /// </summary>
        /// <returns>The value of the current position.</returns>
        public object Clone()
        {
            return new Position(this.Row, this.Column);
        }
    }
}
