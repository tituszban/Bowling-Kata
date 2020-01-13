using System;

namespace Bowling
{
    class Program
    {
        static void Main(string[] args)
        {
            var rolls = new uint[] {0, 0, 0, 0, 0, 0, 0, 0, 0};
            var score = BowlingLine.SubmitRolls(rolls);
            Console.WriteLine("Score: {0}", score.HasValue ? score.Value.ToString() : "Invalid");

        }
    }
}
