using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLife.Logic
{
    public interface IGenerationSrategy
    {
        List<Point> GetAliveCellsForNextGeneration(GameUniverse currentGenerationGameUniverse);
    }
}
