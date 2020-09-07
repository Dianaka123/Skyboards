using System;

namespace Assets.Scripts.Utility
{
    public class FrameRandom
    {
        private readonly Random random = new Random();
        private readonly int size;
        private readonly int frameSize;
        private int prevValue = -1;

        public FrameRandom(int size, int frameSize)
        {
            this.size = size;
            this.frameSize = frameSize;
        }

        public int Generate() => prevValue = Randomize();

        private int Randomize()
        {
            // Start condition.
            if (prevValue == -1)
            {
                return random.Next(0, size - 1);
            }
            else
            {
                var incFrameSize = frameSize + 1;

                var lowBorder = prevValue - incFrameSize;
                var highBorder = prevValue + incFrameSize;

                var lowCount = lowBorder > 0 ? lowBorder + 1 : 0;
                var highCount = highBorder >= size ? 0 : (size - highBorder);
                var totalCount = lowCount + highCount;

                var randomized = random.Next(0, totalCount);

                if (lowCount == 0)
                {
                    return highBorder + randomized;
                }
                else if (randomized >= lowCount)
                {
                    return highBorder + (randomized - lowCount);
                }
                return randomized;
            }
        }

    }
}
