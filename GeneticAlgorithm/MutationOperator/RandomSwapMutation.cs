using System;

namespace GeneticAlgorithm.MutationOperator
{
    public class RandomSwapMutation : IMutationOperator
    {
        private readonly Random rand;
        private readonly double mutationChance;
        private readonly Func<byte[], IIndividual> individualFactory;

        public RandomSwapMutation(Random rand, double mutationChance, Func<byte[], IIndividual> individualFactory)
        {
            this.individualFactory = individualFactory ?? throw new ArgumentNullException(nameof(individualFactory));
            this.rand = rand;
            this.mutationChance = mutationChance;
        }

        public IIndividual Mutate(IIndividual individual)
        {
            if (this.rand.NextDouble() > this.mutationChance)
            {
                var mutatedChromosome = individual.Chromosome;

                var firstIdx = this.rand.Next(0, mutatedChromosome.Length);
                var secondIdx = this.rand.Next(0, mutatedChromosome.Length);
                while (firstIdx == secondIdx)
                {
                    secondIdx = this.rand.Next(0, mutatedChromosome.Length);
                }
                this.Swap(ref mutatedChromosome[firstIdx], ref mutatedChromosome[secondIdx]);
                return this.individualFactory(mutatedChromosome);
            }

            return individual;
        }

        private void Swap<T>(ref T x1, ref T x2)
        {
            T temp = x1;
            x1 = x2;
            x2 = temp;
        }
    }
}
