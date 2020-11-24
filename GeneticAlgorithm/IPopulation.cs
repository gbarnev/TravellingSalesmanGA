using System;
using System.Collections.Generic;
using System.Text;

namespace GeneticAlgorithm
{
    public interface IPopulation
    {
        /// <summary>
        /// Maintains a sorted by fitness collection of the individuals in the population.
        /// Fittest individuals are first.
        /// </summary>
        public IReadOnlyList<IIndividual> Individuals { get; }

        public int Size { get; }
    }
}
