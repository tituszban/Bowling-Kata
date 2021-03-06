using NUnit.Framework;

namespace Bowling.Test
{
    public class BowlingTests
    {
        [TestCase(1u, 1u, 2u)]
        [TestCase(2u, 4u, 6u)]
        [TestCase(9u, 0u, 9u)]
        [TestCase(0u, 9u, 9u)]
        [TestCase(5u, 5u, null)]
        public void SingleFrame(uint roll1, uint roll2, uint? expectedScore)
        {
            var score = BowlingLine.SubmitRolls(new uint[] {roll1, roll2});
            Assert.AreEqual(expectedScore, score);
        }

        [Test]
        public void SingleFrameStrike()
        {
            var score = BowlingLine.SubmitRolls(new uint[] {10u});
            Assert.AreEqual(null, score);
        }

        [TestCase(5u, 0u, 20u)]
        [TestCase(7u, 2u, 26u)]
        [TestCase(0u, 2u, 12u)]
        [TestCase(10u, 5u, null)]
        [TestCase(6u, 4u, null)]
        public void RollsAfterSpare(uint roll1, uint roll2, uint? expectedScore)
        {
            var score = BowlingLine.SubmitRolls(new uint[] {5u, 5u, roll1, roll2});
            Assert.AreEqual(expectedScore, score);
        }

        [TestCase(3u, 4u, 24u)]
        [TestCase(0u, 0u, 10u)]
        [TestCase(4u, 6u, null)]
        public void RollsAfterStrike(uint roll1, uint roll2, uint? expectedScore)
        {
            var score = BowlingLine.SubmitRolls(new uint[] {10u, roll1, roll2});
            Assert.AreEqual(expectedScore, score);
        }

        [TestCase(3u, 4u, 44u)]
        [TestCase(0u, 0u, 30u)]
        [TestCase(8u, 2u, null)]
        [TestCase(10u, 4u, null)]
        public void RollsAfterSpareStrike(uint roll1, uint roll2, uint? expectedScore)
        {
            var score = BowlingLine.SubmitRolls(new uint[] {5u, 5u, 10u, roll1, roll2});
            Assert.AreEqual(expectedScore, score);
        }

        [TestCase(3u, 4u, 47u)]
        [TestCase(9u, 0u, 57u)]
        [TestCase(4u, 6u, null)]
        public void RollsAfterDoubleStrike(uint roll1, uint roll2, uint? expectedScore)
        {
            var score = BowlingLine.SubmitRolls(new uint[] {10u, 10u, roll1, roll2});
            Assert.AreEqual(expectedScore, score);
        }

        [TestCase(new[] {10u, 10u, 10u, 10u, 10u, 10u, 10u, 10u, 10u, 10u, 10u, 10u}, 300u)]
        [TestCase(new[] {9u, 0u, 9u, 0u, 9u, 0u, 9u, 0u, 9u, 0u, 9u, 0u, 9u, 0u, 9u, 0u, 9u, 0u, 9u, 0u}, 90u)]
        [TestCase(new[] {5u, 5u, 5u, 5u, 5u, 5u, 5u, 5u, 5u, 5u, 5u, 5u, 5u, 5u, 5u, 5u, 5u, 5u, 5u, 5u, 5u}, 150u)]
        [TestCase(new[] {10u, 10u, 10u, 10u, 10u, 10u, 10u, 10u, 10u, 10u, 10u}, null)]
        [TestCase(new[] {5u, 5u, 5u, 5u, 5u, 5u, 5u, 5u, 5u, 5u, 5u, 5u, 5u, 5u, 5u, 5u, 5u, 5u, 5u, 5u}, null)]
        public void FullLine(uint[] rolls, uint? expectedScore)
        {
            var score = BowlingLine.SubmitRolls(rolls);
            Assert.AreEqual(expectedScore, score);
        }
    }
}