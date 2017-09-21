using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Drawing;
using System.Collections.Generic;

namespace GameOfLife.Logic.UnitTest
{
    [TestClass]
    public class GameControllerTest
    {
        [TestMethod]
        public void CanCreateGameOfLifeUniverseWithInitialParameters()
        {
            //Setup
            const int _gridXAxisCount = 50;
            const int _gridYAxisCount = 50;
            var aliveCellPoints = new List<Point> { new Point(25, 25) };

            //Act
            var gameUniverse = new GameUniverse(_gridXAxisCount, _gridYAxisCount, aliveCellPoints);

            //Assert
            Assert.AreEqual(gameUniverse.GridXAxisCount, _gridXAxisCount);
            Assert.AreEqual(gameUniverse.GridYAxisCount, _gridYAxisCount);
            Assert.AreEqual(gameUniverse.AliveCells.Count, 1);
        }

        [TestMethod]
        public void CanCreateNewGenerationForSingleAlivePoint()
        {
            //Setup
            const int _gridXAxisCount = 50;
            const int _gridYAxisCount = 50;
            var aliveCellPoints = new List<Point> { new Point(25, 25) };
            IGenerationSrategy generationSrategy = new CandidateDictionaryGenerationStrategy();
            var gameUniverse = new GameUniverse(_gridXAxisCount, _gridYAxisCount, aliveCellPoints);
            var gameController = new GameController(generationSrategy, gameUniverse);

            //Act
            var newGenerationAliveCellPoints = gameController.RunNewGeneration();

            //Assert
            Assert.AreEqual(gameUniverse.GridXAxisCount, _gridXAxisCount);
            Assert.AreEqual(gameUniverse.GridYAxisCount, _gridYAxisCount);
            Assert.AreEqual(newGenerationAliveCellPoints.Count, 0);
        }

        [TestMethod]
        public void AliveCellWithTwoNeighboursSurvivesToNextGeneration()
        {
            //Setup
            const int _gridXAxisCount = 50;
            const int _gridYAxisCount = 50;
            //point [25,25] is the only one with two neighbours
            var aliveCellPoints = new List<Point> { new Point(25, 25), new Point(24, 26), new Point(26, 24) };
            IGenerationSrategy generationSrategy = new CandidateDictionaryGenerationStrategy();
            var gameUniverse = new GameUniverse(_gridXAxisCount, _gridYAxisCount, aliveCellPoints);
            var gameController = new GameController(generationSrategy, gameUniverse);

            //Act
            var newGenerationAliveCellPoints = gameController.RunNewGeneration();

            //Assert
            Assert.AreEqual(gameUniverse.GridXAxisCount, _gridXAxisCount);
            Assert.AreEqual(gameUniverse.GridYAxisCount, _gridYAxisCount);
            Assert.AreEqual(newGenerationAliveCellPoints.Count, 1);
            Assert.IsTrue(newGenerationAliveCellPoints.Contains(new Point(25, 25)));
        }

        [TestMethod]
        public void AliveCellWithThreeNeighboursSurvivesToNextGeneration()
        {
            //Setup
            const int _gridXAxisCount = 60;
            const int _gridYAxisCount = 70;
            //point [17,25] is the only one with three neighbours
            var aliveCellPoints = new List<Point> { new Point(18, 24), new Point(17, 25), new Point(16, 26), new Point(16, 24) };

            IGenerationSrategy generationSrategy = new CandidateDictionaryGenerationStrategy();
            var gameUniverse = new GameUniverse(_gridXAxisCount, _gridYAxisCount, aliveCellPoints);
            var gameController = new GameController(generationSrategy, gameUniverse);

            //Act
            var newGenerationAliveCellPoints = gameController.RunNewGeneration(); ;

            //Assert
            Assert.AreEqual(gameUniverse.GridXAxisCount, _gridXAxisCount);
            Assert.AreEqual(gameUniverse.GridYAxisCount, _gridYAxisCount);
            Assert.AreEqual(newGenerationAliveCellPoints.Count, 3);
            Assert.IsTrue(newGenerationAliveCellPoints.Contains(new Point(17, 25)));
        }

        [TestMethod]
        public void AliveCellWithFourNeighboursIsKilledInNextGeneration()
        {
            //Setup
            const int _gridXAxisCount = 60;
            const int _gridYAxisCount = 70;
            //point [17,25] is the only one with four neighbours
            var aliveCellPoints = new List<Point> { new Point(18, 24), new Point(17, 25), new Point(16, 26), new Point(16, 24), new Point(17, 26) };
            IGenerationSrategy generationSrategy = new CandidateDictionaryGenerationStrategy();
            var gameUniverse = new GameUniverse(_gridXAxisCount, _gridYAxisCount, aliveCellPoints);
            var gameController = new GameController(generationSrategy, gameUniverse);

            //Act
            var newGenerationAliveCellPoints = gameController.RunNewGeneration();

            //Assert
            Assert.AreEqual(gameUniverse.GridXAxisCount, _gridXAxisCount);
            Assert.AreEqual(gameUniverse.GridYAxisCount, _gridYAxisCount);
            Assert.AreEqual(newGenerationAliveCellPoints.Count, 4);
            Assert.IsFalse(newGenerationAliveCellPoints.Contains(new Point(17, 25)));
        }

        [TestMethod]
        public void TwoSetsOfAliveCellWithThreeNeighboursSurvivesToNextGeneration()
        {
            //Setup
            const int _gridXAxisCount = 60;
            const int _gridYAxisCount = 70;
            //point [6,25] and [11,24] are the only one with three neighbours
            var aliveCellPoints = new List<Point> { new Point(7, 24), new Point(6, 25), new Point(5, 26),
                new Point(10, 25), new Point(11, 24), new Point(12, 23) };
            IGenerationSrategy generationSrategy = new CandidateDictionaryGenerationStrategy();
            var gameUniverse = new GameUniverse(_gridXAxisCount, _gridYAxisCount, aliveCellPoints);
            var gameController = new GameController(generationSrategy, gameUniverse);

            //Act
            var newGenerationAliveCellPoints = gameController.RunNewGeneration();

            //Assert
            Assert.AreEqual(gameUniverse.GridXAxisCount, _gridXAxisCount);
            Assert.AreEqual(gameUniverse.GridYAxisCount, _gridYAxisCount);
            Assert.AreEqual(newGenerationAliveCellPoints.Count, 2);
            Assert.IsTrue(newGenerationAliveCellPoints.Contains(new Point(6, 25)));
            Assert.IsTrue(newGenerationAliveCellPoints.Contains(new Point(11, 24)));
        }

        [TestMethod]
        public void DeadCellWithThreeNeighboursSurvivesToNextGeneration()
        {
            //Implies there are other dead cells with two neighbours 
            //thus all cases for dead cells have been accounted for
            
            //Setup
            const int _gridXAxisCount = 60;
            const int _gridYAxisCount = 70;
            //point [6,8] is the only one with three neighbours
            var aliveCellPoints = new List<Point> { new Point(5, 8), new Point(7, 9), new Point(6, 7) };
            IGenerationSrategy generationSrategy = new CandidateDictionaryGenerationStrategy();
            var gameUniverse = new GameUniverse(_gridXAxisCount, _gridYAxisCount, aliveCellPoints);
            var gameController = new GameController(generationSrategy, gameUniverse);

            //Act
            var newGenerationAliveCellPoints = gameController.RunNewGeneration();

            //Assert
            Assert.AreEqual(gameUniverse.GridXAxisCount, _gridXAxisCount);
            Assert.AreEqual(gameUniverse.GridYAxisCount, _gridYAxisCount);
            Assert.AreEqual(newGenerationAliveCellPoints.Count, 1);
            Assert.IsTrue(newGenerationAliveCellPoints.Contains(new Point(6, 8)));
        }
    }
}
