using GameOfLife.Logic;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLife.Simulation
{    
    class Program
    {
        private static StreamWriter _output = new StreamWriter(Console.OpenStandardOutput(), System.Text.Encoding.GetEncoding(437));
        private static char _block = Encoding.GetEncoding(437).GetChars(new byte[] { 219 })[0];
        private const char _whiteSpace = ' ';
        private const string _SessionWillBeClosedMessage = "Session will be closed";
        private const int refreshIntervalInMilliSeconds = 150;

        static void Main(string[] args)
        {
            GameUniverse gameUniverse = null;
            IGenerationSrategy generationSrategy = new CandidateDictionaryGenerationStrategy();
            var intialAliveCellsForOscillatorPattern = new List<Point> { new Point(7, 10), new Point(8, 10),
                                                                            new Point(9, 10), new Point(8, 9) };
            try
            {
                gameUniverse = new GameUniverse(20, 20, intialAliveCellsForOscillatorPattern);
            }
            catch(Exception e)
            {
                DisplayErrorMessageAndCloseScreen(e);
            }

            var gameController = new GameController(generationSrategy, gameUniverse);
            while (!Console.KeyAvailable)
            {
                List<Point> aliveCells = gameController.RunNewGeneration();
                DrawSimulation(aliveCells, gameUniverse);
                System.Threading.Thread.Sleep(refreshIntervalInMilliSeconds);
            }

            Console.ReadKey();
        }

        private static void DrawSimulation(List<Point> aliveCells, GameUniverse gameUniverse)
        {
            InitializeCanvas();
            DrawGameUniverse(aliveCells, gameUniverse);
        }

        private static void DrawGameUniverse(List<Point> aliveCells, GameUniverse gameUniverse)
        {
            for (int rowCount = 0; rowCount < gameUniverse.GridXAxisCount; ++rowCount)
            {
                for (int columnCount = 0; columnCount < gameUniverse.GridYAxisCount; ++columnCount)
                {
                    if (aliveCells.Contains(new Point(rowCount + 1, columnCount + 1)))
                        _output.Write(_block);
                    else
                        _output.Write(_whiteSpace);
                }
                if (rowCount != gameUniverse.GridYAxisCount - 1) _output.Write(Environment.NewLine);
            }

            _output.Flush();
        }

        private static void DisplayErrorMessageAndCloseScreen(Exception e)
        {
            Console.WriteLine(e.Message);
            Console.WriteLine(_SessionWillBeClosedMessage);
            Console.ReadKey();
            Environment.Exit(0);
        }

        private static void InitializeCanvas()
        {
            Console.CursorVisible = false;
            Console.CursorTop = 0;
            Console.CursorLeft = 0;
            Console.ForegroundColor = ConsoleColor.Yellow;
            //Console.BackgroundColor = ConsoleColor.DarkGray;
        }
    }
}