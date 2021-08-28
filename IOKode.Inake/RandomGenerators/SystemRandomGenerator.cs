using System;

namespace IOKode.Inake.RandomGenerators
{
    public class SystemRandomGenerator : IRandomGenerator
    {
        private readonly Random _SystemRandom;

        public SystemRandomGenerator(Random systemRandom)
        {
            _SystemRandom = systemRandom;
        }

        public int Next(int min, int max)
        {
            return _SystemRandom.Next(min, max + 1);
        }
    }
}