using System;
using System.Collections.Generic;
using System.Text;

namespace TeamGame.Test.Domain
{
    public class RandomHelper:Random
    {
        private int count = -1;
        private int[] data = { 1, 1, 0, 0, 1, 1, 3, 3, 5, 1 };

        public RandomHelper(int[] Data)
        {
            data = Data;
        }
        public RandomHelper() { }

        public override int Next(int maxValue)
        {
            count++;
            if (count > data.Length - 1) count = 0;

            return data[count];
        }
    }
}
