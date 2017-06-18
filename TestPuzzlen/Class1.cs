using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;
using NUnit.Compatibility;
using PuzzleN.Model;


namespace TestPuzzlen
{
    [TestFixture]
    public class PuzzlenTest
    {
        [Test]
        public void InitBoard_UniqeNumbers_Ok()
        {
            //Arrange

            Board board = new Board();
            //Art
            List<int> list = new List<int>();
            for (int i = 0; i < board.BoardSize; i++)
            {
                for (int j = 0; j < board.BoardSize; i++)
                {
                    list.Add(board.Matrix[i, j]);
                }
            }

            //Assert

          
            // Assert.(list.Count, list.Distinct().Count);
        }
    }
}
