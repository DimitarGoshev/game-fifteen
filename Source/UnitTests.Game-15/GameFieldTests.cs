using System;
using GameFifteen;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text;

namespace UnitTests.Game_15
{
    [TestClass]
    public class GameFieldTests
    {
        [TestMethod]
        public void GenerateField_IsGeneratedFieldValid()
        {
            const int FIELD_SIZE = 4;
            GameField field = new GameField(FIELD_SIZE, FIELD_SIZE);
            field.GenerateField(new RandomFieldGenerator());
            var existingNumbers = new List<string>();
            bool isFieldValid = true;

            for (int i = 0; i < FIELD_SIZE; i++)
            {
                for (int j = 0; j < FIELD_SIZE; j++)
                {
                    string currentNumber = field[i, j];
                    if (existingNumbers.Contains(currentNumber))
                    {
                        isFieldValid = false;
                        break;
                    }
                    else
                    {
                        existingNumbers.Add(currentNumber);
                    }
                }
            }

            Assert.IsTrue(isFieldValid);
        }

        [TestMethod]
        public void Indexer_NormalCase()
        {
            GameField field = new GameField(4, 4);
            field.GenerateField(new RandomFieldGenerator());
			
            string numberToAssign = "5";
            int row = 2;
            int col = 2;

            field[row, col] = numberToAssign;
            Assert.AreEqual(numberToAssign, field[row, col]);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Indexer_RowsOutOfRange()
        {
            GameField field = new GameField(4, 4);
            field.GenerateField(new RandomFieldGenerator());
            field[-1, 0] = "5";
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Indexer_ColsOutOfRange()
        {
            GameField field = new GameField(4, 4);
            field.GenerateField(new RandomFieldGenerator());
            field[1, 5] = "5";
        }

        [TestMethod]
        public void ToString_GeneralCase()
        {
            GameField field = new GameField(4, 4);
            field.GenerateField(new StubFieldGenerator());

            StringBuilder expected = new StringBuilder();
            expected.AppendLine("* * * * * * * *");
            expected.AppendLine("*  1  2  3  4 *");
            expected.AppendLine("*  5  6  7  8 *");
            expected.AppendLine("*  9 10 11 12 *");
            expected.AppendLine("* 13 14 15    *");
            expected.AppendLine("* * * * * * * *\n\n");

            Assert.AreEqual(expected.ToString(), field.ToString());
        }
    }
}
