using System.Collections.Generic;

namespace GeneticAlgorithm.CrossoverOperator
{
    public interface ICrossoverOperator
    {
        IEnumerable<IIndividual> Process(IIndividual first, IIndividual second);
    }
}
