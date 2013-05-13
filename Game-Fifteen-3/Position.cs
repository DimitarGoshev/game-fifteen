using System;
using System.Linq;

namespace GameFifteen
{
    public class Position : ICloneable
    {
        public Position(int row, int column)
        {
            this.Row = row;
            this.Column = column;
        }

        public int Row { get; set; }

        public int Column { get; set; }

        public object Clone()
        {
            return new Position(this.Row, this.Column);
        }
    }
}
