namespace IOKode.Inake.RandomGenerators
{
    public interface IRandomGenerator
    {
        /// <summary>
        /// Generate number between supplied values.
        /// </summary>
        /// <param name="min">The min value inclusive.</param>
        /// <param name="max">The max value inclusive.</param>
        /// <remarks>Both parameters are inclusive values.</remarks>
        /// <returns></returns>
        public int Next(int min, int max);

        /// <summary>
        /// Generate number between zero and supplied value.
        /// </summary>
        /// <param name="max">The max value inclusive.</param>
        /// <returns></returns>
        public int Next(int max) => Next(0, max);
    }
}