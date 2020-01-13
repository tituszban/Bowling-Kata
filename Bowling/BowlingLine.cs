using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Versioning;
using System.Text;
using System.Threading.Tasks.Sources;

namespace Bowling
{
    public static class BowlingLine
    {
        public static uint? SubmitRolls(uint[] pinsKnockedDown)
        {
            var rolls = new List<uint>(pinsKnockedDown);
            var frames = new List<Frame>();
            while (rolls.Count > 0 && frames.Count < 10)
            {
                var frame = Frame.GetFrame(rolls);
                if (!frame.valid)
                    return null;
                frames.Add(frame);
            }

            return frames.Aggregate<Frame, uint>(0, (current, frame) => current + frame.score.GetValueOrDefault(0));
        }
    }

    internal class Frame
    {
        public uint? score { get; private set; }
        public bool valid { get; private set; } = true;

        public static Frame Invalid => new Frame {valid = false};

        public static Frame Valid(uint score) => new Frame {score = score};

        public static Frame ValidFromRolls(List<uint> nextRolls, int sumNext, int consumeNext)
        {
            var score = Enumerable.Range(0, sumNext).Aggregate<int, uint>(0u, (current, i) => current + nextRolls[i]);
            nextRolls.RemoveRange(0, consumeNext);
            return Frame.Valid(score);
        }

        public static Frame GetFrame(List<uint> nextRolls)
        {
            if (nextRolls.Count < 2)
                return Frame.Invalid;

            if (nextRolls[0] == 10)
                return nextRolls.Count < 3 ? Frame.Invalid : Frame.ValidFromRolls(nextRolls, 3, 1);

            if (nextRolls[0] + nextRolls[1] == 10)
                return nextRolls.Count < 3 ? Frame.Invalid : Frame.ValidFromRolls(nextRolls, 3, 2);

            return Frame.ValidFromRolls(nextRolls, 2, 2);
        }
    }
}