using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLife.Logic
{
    public class GameController
    {
        private IGenerationSrategy _generationSrategy = new CandidateDictionaryGenerationStrategy();
        private GameUniverse _gameUniverse;

        // TODO - set up DI container to discover Generation Strategy     
        public GameController(IGenerationSrategy generationSrategy, GameUniverse gameUniverse)
        {
            _generationSrategy = generationSrategy;
            _gameUniverse = gameUniverse;
        }

        /// <summary>
        /// Executes one generation for any given state of its GameUniverse
        /// </summary>
        /// <returns>Return Alive cells for new generation</returns>
        public List<Point> RunNewGeneration()
        {
            List<Point> newGenerationAliveCells = _generationSrategy.GetAliveCellsForNextGeneration(_gameUniverse);
            _gameUniverse.AliveCells = new List<Point>(newGenerationAliveCells);

            return newGenerationAliveCells;
        }
    }
}
