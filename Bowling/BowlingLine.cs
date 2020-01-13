using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Versioning;
using System.Text;

namespace Bowling
{
    public static class BowlingLine
    {
        public static uint? SubmitRolls(uint[] pinsKnockedDown)
        {
            int pointer = 0;
            var frames = new List<Frame>();
            while (pointer < pinsKnockedDown.Length && frames.Count < 10)
            {
                var frame = Frame.GetFrame(pinsKnockedDown.Skip(pointer).ToArray());
                if (!frame.valid)
                    return null;
                frames.Add(frame);
                pointer += frame.pointerMove;
            }

            return frames.Aggregate<Frame, uint>(0, (current, frame) => current + frame.score.GetValueOrDefault(0));
        }
    }

    class Frame
    {
        public int pointerMove { get; private set; }
        public uint? score { get; private set; }
        public bool valid { get; private set; }

        public static Frame Invalid => new Frame {valid = false, pointerMove = 0};

        public static Frame GetFrame(uint[] nextRolls)
        {
            if (nextRolls.Length <= 0)
                return Frame.Invalid;

            if (nextRolls[0] == 10)
            {
                if (nextRolls.Length < 3)
                    return Frame.Invalid;
                return new Frame {valid = true, score = 10 + nextRolls[1] + nextRolls[2], pointerMove = 1};
            }

            if (nextRolls.Length < 2)
                return Frame.Invalid;

            if (nextRolls[0] + nextRolls[1] == 10)
            {
                if (nextRolls.Length < 3)
                    return Frame.Invalid;
                return new Frame {valid = true, score = 10 + nextRolls[2], pointerMove = 2};
            }

            return new Frame {valid = true, score = nextRolls[0] + nextRolls[1], pointerMove = 2};

        }
    }
}