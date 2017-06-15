using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PuzzleN.Model
{
    public class Board
    {
      
        public int BoardSize;
        public int[,] Matrix;

        public int[,] InitBoard(int boardsize , bool firsttime)
        {
            BoardSize = boardsize;
            Matrix = new int[boardsize, boardsize];
            if (firsttime)
            { 
             while (isSolvable(InternalInitBoard(boardsize)))
                 return Matrix;
            }

            return Matrix;

        }

        private int[,] InternalInitBoard(int boardsize)
        {
            int[,] matrix = new int[boardsize, boardsize];
           
            int[] randomArray =CreateRandomArray();

            Matrix = FillMatrixFromArray(matrix, randomArray);
            
            return Matrix;
        }

        private void SwapCells(int x1, int y1, int x2, int y2)
        {
            int CurrentTile = new int();
            CurrentTile = Matrix[x1, y1];
            Matrix[x1, y1] = Matrix[x2, y2];
            Matrix[x2, y2] = CurrentTile;

        }
        public bool MoveTile(int x, int y)
        {
            if (TileCanMoveDown(x, y))
            {
                SwapCells(x, y, x, y + 1);
                return true;
            }
            if (TileCanMoveUp(x, y))
            {
                SwapCells(x, y, x, y - 1);
                return true;
            }
            if (TileCanMoveRight(x, y))
            {
                SwapCells(x, y, x + 1, y);
                return true;
            }
            if (TileCanMoveLeft(x, y))
            {
                SwapCells(x, y, x - 1, y );
                return true;
            }
            return false;

        }
        private bool TileCanMoveDown(int x, int y)
        {
            if ((y + 1) >= BoardSize) //check border
                return false;
            else
            {
                if (Matrix[x, y + 1] == -1) // check blank cell
                    return true;
                else
                    return false;
            }
        }
        private bool TileCanMoveUp(int x, int y)
        {
            if ((y - 1) < 0) //check border
                return false;
            else
            {
                if (Matrix[x, y - 1] == -1) // check blank cell
                    return true;
                else
                    return false;
            }
        }
        private bool TileCanMoveRight(int x, int y)
        {
            if ((x + 1) >= BoardSize) //check border
                return false;
            else
            {
                if (Matrix[x + 1, y] == -1) // check blank cell
                    return true;
                else
                    return false;
            }
        }

        private bool TileCanMoveLeft(int x, int y)
        {
            if ((x - 1) < 0) //check border
                return false;
            else
            {
                if (Matrix[x - 1, y] == -1) // check blank cell
                    return true;
                else
                    return false;
            }
        }

        private int[] CreateRandomArray()
        {
            int[] x = new int[BoardSize * BoardSize];
            Random r = new Random();

            int zi;
            for (zi = 0; zi < x.Length; zi++)
            {
                var next = 0;
                while (true)
                {
                    next = r.Next(BoardSize * BoardSize + 1);
                    if (!Contains(x, next)) break;
                }

                x[zi] = next;


            }
            return x;
        }
        private bool Contains(int[] array, int value)
        {
            for (int i = 0; i < array.Length; i++)
            {
                if (array[i] == value) return true;
            }
            return false;
        }

        public int[,] FillMatrixFromArray(int[,] matrix, int[] rArray)
        {
            int counter = 0;
            for (int i = 1; i <= BoardSize; i++)
            {
                for (int j = 1; j <= BoardSize; j++)
                {
                    if (rArray[counter] == BoardSize * BoardSize)
                        rArray[counter] = -1;
                    matrix[j - 1, i - 1] = rArray[counter];
                    counter++;
                }
            }
            this.Matrix = matrix;
            return matrix;
        }

        private bool isSolvable(int[,] matrix)
        {
            int[] puzzle = ConvertMetrixtoArray(matrix);
            int parity = 0;
            int gridWidth = (int)Math.Sqrt(puzzle.Length);
            int row = 0; // the current row we are on
            int blankRow = 0; // the row with the blank tile

            for (int i = 0; i < puzzle.Length; i++)
            {
                if (i % gridWidth == 0)
                { // advance to next row
                    row++;
                }
                if (puzzle[i] == -1)
                { // the blank tile
                    blankRow = row; // save the row on which encountered
                    continue;
                }
                for (int j = i + 1; j < puzzle.Length; j++)
                {
                    if (puzzle[i] > puzzle[j] && puzzle[j] != 0)
                    {
                        parity++;
                    }
                }
            }

            if (gridWidth % 2 == 0)
            { // even grid
                if (blankRow % 2 == 0)
                { // blank on odd row; counting from bottom
                    return parity % 2 == 0;
                }
                else
                { // blank on even row; counting from bottom
                    return parity % 2 != 0;
                }
            }
            else
            { // odd grid
                return parity % 2 == 0;
            }
        }

        private int[] ConvertMetrixtoArray(int[,] matrix)
        {
            int counter = 0;
            int[] arr = new int[BoardSize* BoardSize];
            for (int i = 1; i <= BoardSize; i++)
            {
                for (int j = 1; j <= BoardSize; j++)
                {
                    arr[counter] = matrix[j - 1, i - 1];
                    counter++;
                }
            }
            return arr;

        }

        public bool CheckSolved(int[,] matrix)
        {
            int[] puzzle = ConvertMetrixtoArray(matrix);

            for( int i=0;i< puzzle.Length-1;i++)
            {

                if (puzzle[i] != i+1)
                    return false;
            }
            return true;
        }
    }

}

