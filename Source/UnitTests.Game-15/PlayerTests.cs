using System;
using GameFifteen;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests.Game_15
{
    [TestClass]
    public class PlayerTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Constructor_InvalidName()
        {
            var player = new Player("", 12);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Constructor_InvalidScore()
        {
            var player = new Player("pesho", -10);
        }
    }
}
