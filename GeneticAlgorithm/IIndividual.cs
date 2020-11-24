using System;

namespace GeneticAlgorithm
{
    public interface IIndividual : IComparable<IIndividual>
    {
        public string Id { get; }

        public byte[] Chromosome { get; }

        public int Fitness { get; }
    }
}
