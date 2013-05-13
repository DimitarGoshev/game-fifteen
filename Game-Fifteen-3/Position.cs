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

<<<<<<< HEAD
        public int Row { get; set; }

        public int Column { get; set; }

=======
>>>>>>> d89775755846ff86fdeec24da24512ef49fe1038
        public object Clone()
        {
            return new Position(this.Row, this.Column);
        }
    }
}
