using System;
using GameFifteen;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

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
            field.GenerateField();
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
            field.GenerateField();
			
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
            field.GenerateField();
            field[-1, 0] = "5";
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Indexer_ColsOutOfRange()
        {
            GameField field = new GameField(4, 4);
            field.GenerateField();
            field[1, 5] = "5";
        }
    }
}
