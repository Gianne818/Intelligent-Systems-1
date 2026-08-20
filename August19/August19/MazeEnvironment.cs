using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace August19
{

    //define a node
    public struct Node
    {
        public int Row { get; }
        public int Col { get; }

        public Node(int row, int col)
        {
            Row = row;
            Col = col;
        }

        public override string ToString()
        {
            return $"({Row}, {Col})";
        }
    }

    // define the problem: a 5x5 maze
    public static class MazeSolver
    {
        // 0 = path, 1 = obstacle
        private static readonly int[,] Grid =
        {
            {0, 0, 1, 0, 0 },
            {0, 0, 1, 0, 0 },
            {0, 1, 1, 0, 1 },
            {0, 0, 0, 0, 0 },
            {1, 1, 0, 1, 0 }

        };

        private static readonly int rows = Grid.GetLength(0);
        private static readonly int cols = Grid.GetLength(1);

        public static IEnumerable GetNeighbors(Node n)
        {
            int[] dRow = { -1, 1, 0, 0 }; // Up(-1 from row), Down(+1 from row)
            int[] dCol = { 0, 0, -1, 1 }; // Left(-1 from col), Right(+1 from col)

            for (int i = 0; i < 4; i++)
            {
                int nRow = n.Row + dRow[i];
                int nCol = n.Col + dCol[i];

                // check for boundaries

                if (nRow >= 0 && nRow < rows &&
                    nCol >= 0 && nCol < cols &&
                    Grid[nRow, nCol] == 0)
                {
                    yield return new Node(nRow, nCol);
                }
            }
        }

        public static int[,] GetGrid()
        {
            return Grid;
        }
    }
}
