using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace GameOfLife.Logic
{
    public class GameUniverse
    {
        private List<Point> _aliveCells;
        private readonly int _gridXAxisCount;
        private readonly int _gridYAxisCount;

        public int GridXAxisCount { get => _gridXAxisCount; }
        public int GridYAxisCount { get => _gridYAxisCount; }
        public List<Point> AliveCells { get => _aliveCells; internal set => _aliveCells = value; }

        public GameUniverse(int gridXAxisCount, int gridYAxisCount, List<Point> aliveCellsForFirstGeneration)
        {
            EnsureArgumentsAreWithinRange(gridXAxisCount, gridYAxisCount, aliveCellsForFirstGeneration);

            _gridXAxisCount = gridXAxisCount;
            _gridYAxisCount = gridYAxisCount;
            _aliveCells = aliveCellsForFirstGeneration;
        }

        private void EnsureArgumentsAreWithinRange(int gridXAxisCount, int gridYAxisCount, List<Point> aliveCellsForFirstGeneration)
        {
            foreach (var aliveCell in aliveCellsForFirstGeneration)
            {
                if (!Enumerable.Range(1, gridXAxisCount).Contains(aliveCell.X))
                    throw new ArgumentOutOfRangeException("gridXAxisCount",
                        $"gridXAxisCount should be a value between 1 and {gridXAxisCount}");

                if (!Enumerable.Range(1, gridYAxisCount).Contains(aliveCell.Y))
                    throw new ArgumentOutOfRangeException("gridYAxisCount",
                        $"gridYAxisCount should be a value between 1 and {gridYAxisCount}");
            }
        }
    }
}
