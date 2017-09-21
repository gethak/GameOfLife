using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLife.Logic
{
    public class CandidateDictionaryGenerationStrategy : IGenerationSrategy
    {
        private Dictionary<Point, int> _aliveNeighboursCountForCurrentlyAlivePoints;
        private Dictionary<Point, int> _aliveNeighboursCountForCurrentlyDeadPoints;
        private GameUniverse _gameUniverse;
        private List<VisitablePoint> _aliveCellPoints;

        public CandidateDictionaryGenerationStrategy()
        { }

        public List<Point> GetAliveCellsForNextGeneration(GameUniverse currentGenerationGameUniverse)
        {
            InitializeMembers(currentGenerationGameUniverse);            
            CalculateCandidatePointsForNextGeneration();
            List<Point> alivePointsForNextGenerationThatAreCurrentlyDead = GetAlivePointsForNextGenerationThatAreCurrentlyDead();
            List<Point> alivePointsForNextGenerationThatAreCurrentlyAlive = GetAlivePointsForNextGenerationThatAreCurrentlyAlive();
            var alivePointsForNextGeneration = new List<Point>(alivePointsForNextGenerationThatAreCurrentlyDead);
            alivePointsForNextGeneration.AddRange(alivePointsForNextGenerationThatAreCurrentlyAlive);

            return alivePointsForNextGeneration;
        }

        private void InitializeMembers(GameUniverse currentGenerationGameUniverse)
        {
            _aliveNeighboursCountForCurrentlyAlivePoints = new Dictionary<Point, int>();
            _aliveNeighboursCountForCurrentlyDeadPoints = new Dictionary<Point, int>();

            _gameUniverse = currentGenerationGameUniverse;
            _aliveCellPoints = currentGenerationGameUniverse.AliveCells.Select(x => new VisitablePoint(x)).ToList();
        }

        private List<Point> GetAlivePointsForNextGenerationThatAreCurrentlyAlive()
        {
            return _aliveNeighboursCountForCurrentlyAlivePoints.Where(x => x.Value == 2 || x.Value == 3)
                                                                 .Select(x => x.Key).ToList();
        }

        private List<Point> GetAlivePointsForNextGenerationThatAreCurrentlyDead()
        {
            return _aliveNeighboursCountForCurrentlyDeadPoints.Where(x => x.Value == 3)
                                                                 .Select(x => x.Key).ToList();
        }

        private void CalculateCandidatePointsForNextGeneration()
        {
            _aliveCellPoints.ForEach(aliveCellPoint =>
            {
                DiscoverNeighboursAndIncrementCount(aliveCellPoint);
                MarkAliveCellPointAsVisited(aliveCellPoint);
            });
        }

        private void MarkAliveCellPointAsVisited(VisitablePoint aliveCellPoint)
        {
            _aliveCellPoints.
                Single(cell => cell.X == aliveCellPoint.X && cell.Y == aliveCellPoint.Y).IsVisited = true;
        }

        private void DiscoverNeighboursAndIncrementCount(VisitablePoint aliveCellPoint)
        {
            if (aliveCellPoint.X < _gameUniverse.GridXAxisCount)
            {
                Point immediateRight = new Point(aliveCellPoint.X + 1, aliveCellPoint.Y);
                IncrementAliveCountIfUnvisitedAliveNeighbour(aliveCellPoint, immediateRight);
            }

            if (aliveCellPoint.X > 1)
            {
                Point immediateLeft = new Point(aliveCellPoint.X - 1, aliveCellPoint.Y);
                IncrementAliveCountIfUnvisitedAliveNeighbour(aliveCellPoint, immediateLeft);
            }

            if (aliveCellPoint.Y > 1)
            {
                Point immediateTop = new Point(aliveCellPoint.X, aliveCellPoint.Y - 1);
                IncrementAliveCountIfUnvisitedAliveNeighbour(aliveCellPoint, immediateTop);
            }

            if (aliveCellPoint.Y < _gameUniverse.GridYAxisCount)
            {
                Point immediateBottom = new Point(aliveCellPoint.X, aliveCellPoint.Y + 1);
                IncrementAliveCountIfUnvisitedAliveNeighbour(aliveCellPoint, immediateBottom);
            }

            if (aliveCellPoint.X < _gameUniverse.GridXAxisCount && aliveCellPoint.Y > 1)
            {
                Point diagonalTopRight = new Point(aliveCellPoint.X + 1, aliveCellPoint.Y - 1);
                IncrementAliveCountIfUnvisitedAliveNeighbour(aliveCellPoint, diagonalTopRight);
            }

            if (aliveCellPoint.X < _gameUniverse.GridXAxisCount && aliveCellPoint.Y < _gameUniverse.GridYAxisCount)
            {
                Point diagonalBottomRight = new Point(aliveCellPoint.X + 1, aliveCellPoint.Y + 1);
                IncrementAliveCountIfUnvisitedAliveNeighbour(aliveCellPoint, diagonalBottomRight);
            }

            if (aliveCellPoint.X > 1 && aliveCellPoint.Y > 1)
            {
                Point diagonalTopLeft = new Point(aliveCellPoint.X - 1, aliveCellPoint.Y - 1);
                IncrementAliveCountIfUnvisitedAliveNeighbour(aliveCellPoint, diagonalTopLeft);
            }

            if (aliveCellPoint.X > 1 && aliveCellPoint.Y < _gameUniverse.GridYAxisCount)
            {
                Point diagonalBottomLeft = new Point(aliveCellPoint.X - 1, aliveCellPoint.Y + 1);
                IncrementAliveCountIfUnvisitedAliveNeighbour(aliveCellPoint, diagonalBottomLeft);
            }
        }

        private void IncrementAliveCountIfUnvisitedAliveNeighbour(VisitablePoint aliveCellPoint, Point point)
        {
            if (IsAlivePoint(point))
            {
                //currently alive neighbor
                if (!IsVisitedPoint(point))
                {
                    IncrementNeighborCount(aliveCellPoint, point, true);
                }
            }

            //currently Dead neighbor
            else IncrementNeighborCount(point);
        }

        private bool IsVisitedPoint(Point Point)
        {
            //Exception should be thrown if not Single here
            return _aliveCellPoints.Single(x => x.X == Point.X && x.Y == Point.Y).IsVisited;
        }

        private bool IsAlivePoint(Point immediateRight)
        {
            return _gameUniverse.AliveCells.Contains(immediateRight);
        }

        private void IncrementNeighborCount(VisitablePoint aliveCellPoint, Point Point, bool useDictionaryForCurrentlyAlivePoints = false)
        {
            IncrementNeighborCount(aliveCellPoint.Point, true);
            IncrementNeighborCount(Point, true);
        }

        private void IncrementNeighborCount(Point point, bool useDictionaryForCurrentlyAlivePoints = false)
        {
            Dictionary<Point, int> dictionaryToIncrement = useDictionaryForCurrentlyAlivePoints ?
                _aliveNeighboursCountForCurrentlyAlivePoints : _aliveNeighboursCountForCurrentlyDeadPoints;            

            if (!dictionaryToIncrement.Keys.Contains(point))
                dictionaryToIncrement[point] = 0;

            dictionaryToIncrement[point] += 1;
        }
    }
}
