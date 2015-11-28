using System;
using System.Collections.Generic;
using System.Text;

namespace NeuralNetwork
{
    class StaticRandom
    {
        [ThreadStatic]
        static Random random;
        public static Random Random
        {
            get
            {
                if (random == null) random = new Random();
                return random;
            }
        }
        public static int RandomInteger(int min, int max)
        {
            return Random.Next(min, max);
        }
        public static double RandomDouble() //Between 0 and 1
        {
            return Random.NextDouble();
        }
    }
}
