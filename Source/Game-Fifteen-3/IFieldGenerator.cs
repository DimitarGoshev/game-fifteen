using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameFifteen
{
    public interface IFieldGenerator
    {
        string[,] GenerateField(int rows, int cols);
    }
}
