using ConsoleApp4;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CannonGame
{
    [TestFixture]
    class GameShould
    {

        [Test]
        public static void MouseCalculationTest()
        {
            var res = Helper.CalculateDistances(315, 5, 283, 273);
            Assert.AreEqual(res.Item1, 31.016124838541646);
            Assert.AreEqual(res.Item2, 83.190949820386592);
        }

        [Test]
        public static void BallGravityMoveTest()
        {
            var ball = new Ball(0,0,5);
            Constants.gravity = new Vector(0, 0.4);
            ball.Move();
            Assert.AreEqual(ball.position.X, 0);
            Assert.AreEqual(ball.position.Y, 0.4);
        }

        [Test]
        public static void BallGravityAndVelocityMoveTest()
        {
            var ball = new Ball(0, 0, 5);
            Constants.gravity = new Vector(0, 0.4);
            ball.velocity = new Vector(0.5, 0);
            ball.Move();
            Assert.AreEqual(ball.position.X, 0.5);
            Assert.AreEqual(ball.position.Y, 0.4);
        }

        [Test]
        public static void CheckBallWallsColisionTest()
        {
            Constants.WindowWidth = 500;
            Constants.WindowHeight = 400;
            var ball = new Ball(-5, 0, 5);
            Assert.AreEqual(ball.CheckWallCollision(), true);
            var ball2 = new Ball(5, 5, 5);
            Assert.AreEqual(ball2.CheckWallCollision(), false);
        }

        [Test]
        public static void CheckBallGroundColisionTest()
        {
            var ball = new Ball(15, 0, 5);
            ball.velocity = new Vector(-5, 0);
            Assert.AreEqual(ball.CheckGroundCollision(new Ground(0,0, 20, 20)), false);
            Assert.AreEqual(ball.velocity.X, -5);

            var ball2 = new Ball(5, 5, 5);
            ball2.velocity = new Vector(-5, 0);
            Assert.AreEqual(ball2.CheckGroundCollision(new Ground(0, 0, 20, 20)), true);
            Assert.AreEqual(ball2.velocity.X, -1.75, 0.00001);
        }
    }
}
