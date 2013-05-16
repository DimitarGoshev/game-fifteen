using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameFifteen;

namespace UnitTests.Game_15
{
    /// <summary>
    /// Generates the game field ordered every time and makes it easier to test
    /// </summary>
    class StubFieldGenerator : IFieldGenerator
    {
        public string[,] GenerateField(int rows, int cols)
        {
            return new string[4, 4]
            {
                {"1", "2", "3", "4"},
                {"5", "6", "7", "8"},
                {"9", "10", "11", "12"},
                {"13", "14", "15", " "}
            };
        }
    }
}
